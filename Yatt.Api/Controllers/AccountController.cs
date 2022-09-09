using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using Yatt.Api.Handlers;
using Yatt.Api.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IAccountRepository _accountrepository { get; }
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IUserTokenRepository _userTokenRepository;

        public AccountController(IAccountRepository repository, ITokenManager tokenManager,
            IUserTokenRepository userTokenRepository, IRefreshTokenRepository refTokenRepository)
        {
            _accountrepository = repository;
            this._refreshTokenRepository = refTokenRepository;
            this._tokenManager = tokenManager;
            this._userTokenRepository = userTokenRepository;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto userModel)
        {
            var message = string.Empty;
            if (ModelState.IsValid)
            {
                var response = await _accountrepository.Register(userModel);
                if (response!.Model != null && response.Status == ResponseStatus.Success)
                {
                    var ipAddress = HttpContext.Connection.RemoteIpAddress;
                    var userAgent = HttpContext.Request.Headers["User-Agent"];

                    var refreshToken = await _userTokenRepository
                       .GenerateEmailConfirmationToken(new RequestTokenDto
                       {
                           UserId = response.Model.Id,
                           UserAgent = userAgent,
                           IpAddress = ipAddress!.ToString(),
                       });
                    return Ok(new ResponseDto<AuthDto>
                    {
                        Model = response.Model,
                        Status = ResponseStatus.Success,
                        Message = refreshToken.Message
                    });
                }
                message = response.Message;
            }
            return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Eerror occured. {message}" });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response = await _accountrepository.VerifyUser(login);
            if (response.Status != ResponseStatus.Success)
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Unautorize, Message = response.Message });

            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            var userAgent = HttpContext.Request.Headers["User-Agent"];

            AuthDto? user = response.Model;


            try
            {
                var refreshToken = await _refreshTokenRepository
                .UpdateRefreshToken(new RequestTokenDto
                {
                    UserId = user!.Id,
                    UserAgent = userAgent,
                    IpAddress = ipAddress!.ToString()
                });

                if (refreshToken != null && refreshToken.IpAddress != ipAddress.ToString())
                {
                    // TODO Send notification to the user and the admin => May be  Hacker attck.
                }
                var token = await _tokenManager.GenerateToken(user);

                user.RefreshToken = refreshToken!.Token;
                user.TokenExpireTime = DateTime.UtcNow.AddHours(10);
                user.Token = token;
                var dto = new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
                return Ok(dto);
            }
            catch (Exception exp)
            {
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = exp.Message });
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RequestRefreshTokenDto tokenDto)
        {
            if (tokenDto is null)
            {
                return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = "Invalid client request" });
            }
            ClaimsPrincipal? principal;
            Claim? userClaim;
            AuthDto? user;
            try
            {
                principal = _tokenManager.GetPrincipalFromExpiredToken(tokenDto.Token);
                if (principal == null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Invalid token" });

                userClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
                if (userClaim == null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Claim not found" });

                var response = await _accountrepository.GetUser(userClaim.Value);
                if (response.Status != ResponseStatus.Success || response.Model == null)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "User not found" });

                user = response.Model;
                var ipAddress = HttpContext.Connection.RemoteIpAddress;
                var userAgent = HttpContext.Request.Headers["User-Agent"];

                var refreshToken = await _refreshTokenRepository
                   .GetRefreshToken(tokenDto.RefreshToken, userAgent);

                if (refreshToken == null || refreshToken.Token != tokenDto.RefreshToken || refreshToken.TokenExpireTime <= DateTime.Now)
                    return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Invalid client request" });
                var refreshResponse = await _refreshTokenRepository
                    .UpdateRefreshToken(new RequestTokenDto
                    {
                        UserId = user.Id,
                        IpAddress = ipAddress!.ToString(),
                        Token = refreshToken.Token,
                        UserAgent = userAgent
                    });

                var token = await _tokenManager.GenerateToken(user);
                user.RefreshToken = refreshToken.Token;

                return Ok(new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = ex.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("ConfirmEmail/{id}")]
        public async Task<IActionResult> ConfirmEmail(string id, [FromBody] ConfirmDto confirmDto)
        {
            if (id != confirmDto.UserId)
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = "The ID's didnt much" });
            var message = string.Empty;
            try
            {
                var response = await _accountrepository.ConfirmEmail(confirmDto);
                return Ok(response);
            }
            catch (Exception exp)
            {
                return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Unkown error occured. {exp.Message}" });
            }
        }

        [AllowAnonymous]
        [HttpPost("RegisterPhone/{id}")]
        public async Task<IActionResult> SendPhoneConfirmation(string id)
        {
            if (id == null)
                return Ok(new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = "The ID's didnt much" });
            var message = string.Empty;
            try
            {
                var response = await _accountrepository.SendPhoneConfirmation(id);
                // SEND SMS TO USER
                return Ok(response);
            }
            catch (Exception exp)
            {
                return BadRequest(new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = $"Unkown error occured. {exp.Message}" });
            }
        }
        [AllowAnonymous]

        [HttpPost("ConfirmPhone/{id}")]
        public async Task<IActionResult> ConfirmPhone(string id, [FromBody] ConfirmDto confirmDto)
        {
            if (id != confirmDto.UserId)
                return Ok(new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = "The ID's didnt much" });
            var message = string.Empty;
            try
            {
                var response = await _accountrepository.ConfirmPhone(confirmDto);
                return Ok(response);
            }
            catch (Exception exp)
            {
                return Ok(new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = $"Unkown error occured. {exp.Message}" });
            }
        }

        [HttpGet("userPagedList")]
        public async Task<IActionResult> GetPagedList([FromQuery] PageParameter pageParameter)
        {
            var users = await _accountrepository.GetUserPagedList(pageParameter);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(users.MetaData));
            return Ok(users);
        }

        // COMPANY REGISTRATION
        [AllowAnonymous]
        [HttpPost("RegisterCompany")]
        public async Task<IActionResult> CompanyRegister([FromBody] RegisterCompanyDto userModel)
        {
            var message = string.Empty;
            if (ModelState.IsValid)
            {
                var response = await _accountrepository.RegisterCompany(userModel);
                if (response!.Model != null && response.Status == ResponseStatus.Success)
                {
                    var ipAddress = HttpContext.Connection.RemoteIpAddress;
                    var userAgent = HttpContext.Request.Headers["User-Agent"];

                    var refreshToken = await _userTokenRepository
                       .GenerateEmailConfirmationToken(new RequestTokenDto
                       {
                           UserId = response.Model.Id,
                           UserAgent = userAgent,
                           IpAddress = ipAddress!.ToString(),
                       });
                    return Ok(new ResponseDto<AuthDto>
                    {
                        Model = response.Model,
                        Status = ResponseStatus.Success,
                        Message = refreshToken.Message
                    });
                }
                message = response.Message;
            }
            return Ok(new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Eerror occured. {message}" });
        }
    }
}
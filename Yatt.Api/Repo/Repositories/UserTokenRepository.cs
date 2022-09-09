using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Api.Repo.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        public AppDbContext _context { get; }
        public UserTokenRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ResponseDto<UserTokenDto>>
            GenerateEmailConfirmationToken(RequestTokenDto dto)
        {
            UserToken? tokenModel = await _context.UserTokens
                .FirstOrDefaultAsync(a => a.UserId == dto.UserId && a.TokenType == TokenType.EmailConfirmation);
            var current = DateTime.UtcNow;
            if (tokenModel == null)
            {
                tokenModel = new UserToken
                {
                    Id=Guid.NewGuid().ToString(),
                    UserId = dto.UserId,
                    Token = Guid.NewGuid().ToString(),
                    ModifyDate = current,
                    CreatedDate = current,
                    IpAddress = dto.IpAddress,
                    UserAgent = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web",
                    DeletedDate = null,
                    TokenType = TokenType.EmailConfirmation
                };
                _context.UserTokens.Add(tokenModel);
            }
            else
            {
                tokenModel.ModifyDate = current;
                tokenModel.Token = Guid.NewGuid().ToString();
                tokenModel.IpAddress = dto.IpAddress;
                tokenModel.UserAgent = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web";
            }

            try
            {
                await _context.SaveChangesAsync();

                return await Task.FromResult(new ResponseDto<UserTokenDto>
                {
                    Model = tokenModel,
                    Status = ResponseStatus.Success,
                    Message = $"Confirm email: UserId: {tokenModel.UserId} Token : {tokenModel.Token}"
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseDto<UserTokenDto> { Status = ResponseStatus.Error, Message = $"Error occured: {ex.Message}" });
            }

        }

        public Task<ResponseDto<UserTokenDto>> GeneratePhoneConfirmationToken(AuthDto dto)
        {
            throw new NotImplementedException();
        }
        private string GenerateRefreshToken()
        {
            var rand = new Random();

            //var randomNumber = new byte[4];
            //using (var rng = RandomNumberGenerator.Create())
            //{
            //    rng.GetBytes(randomNumber);
            //    return Convert.ToBase64String(randomNumber);
            //}
            var str = rand.Next(999999).ToString();
            return str.Insert(3, "-");
        }

        public async Task<ResponseDto<RefreshTokenDto>> GetRefreshToken(RequestTokenDto dto)
        {
            string incomeSource = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web";

            var userToken = await _context.RefreshTokens
                .Where(a => a.Token == dto.Token &&
                    a.IpAddress == dto.IpAddress)
                .FirstOrDefaultAsync();

            if (userToken == null)
                return new ResponseDto<RefreshTokenDto> { Model = userToken!, Status = ResponseStatus.NotFound };

            return new ResponseDto<RefreshTokenDto> { Model = userToken!, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<RefreshTokenDto>> GetRefreshTokenByUserId(RequestTokenDto dto)
        {
            string incomeSource = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web";
            var userToken = await _context.RefreshTokens.Where(a => a.UserId == dto.UserId && a.UserAgent == incomeSource).FirstOrDefaultAsync();

            if (userToken == null)
                return new ResponseDto<RefreshTokenDto> { Status = ResponseStatus.NotFound };

            return new ResponseDto<RefreshTokenDto> { Model = userToken!, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<RefreshTokenDto>> UpdateRefreshToken(RequestTokenDto dto)
        {
            var tokenModel = await GetRefreshToken(dto);

            RefreshToken? token = new RefreshToken();

            if (tokenModel.Status == ResponseStatus.Success)
            {
                token = await _context.RefreshTokens.FirstOrDefaultAsync(a => a.Id == tokenModel.Model!.Id);
                token!.Token = Guid.NewGuid().ToString();
                token.ModifyDate = DateTime.Now;

                _context.RefreshTokens.Update(token);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return new ResponseDto<RefreshTokenDto> { Status = ResponseStatus.Error, Message = ex.Message };
                }
            }
            else if (tokenModel.Status == ResponseStatus.NotFound)
            {
                return new ResponseDto<RefreshTokenDto> { Status = ResponseStatus.NotFound };
            }

            return new ResponseDto<RefreshTokenDto> { Model = token!, Status = ResponseStatus.Success };
        }

        public async Task<ResponseDto<RefreshTokenDto>> AddRefreshToken(RequestTokenDto dto)
        {
            var now = DateTime.Now;
            RefreshToken tokenModel = new RefreshToken
            {
                UserId = dto.UserId,
                Token = Guid.NewGuid().ToString(),
                ModifyDate = now,
                CreatedDate = now,
                IpAddress = dto.IpAddress,
                UserAgent = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web",
                DeletedDate = null
            };
            _context.RefreshTokens.Add(tokenModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDto<RefreshTokenDto> { Status = ResponseStatus.Error, Message = ex.Message };
            }

            return new ResponseDto<RefreshTokenDto> { Model = tokenModel, Status = ResponseStatus.Success, };
        }
    }
}

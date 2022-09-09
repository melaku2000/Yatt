using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Yatt.Api.Data;
using Yatt.Api.Handlers;
using Yatt.Api.Repo.Interfaces;
using Yatt.Api.Repo.Repositories.Extensions;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;

namespace Yatt.Api.Repo.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public AppDbContext _context { get; }
        private readonly Random _random = new Random();
        public AccountRepository(AppDbContext repository)
        {
            _context = repository;
        }

        private async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .Include(a=>a.Candidate)
                .Include(a=>a.Role).FirstOrDefaultAsync(x => x.Email == email!.ToLower());
        }

        public async Task<ResponseDto<AuthDto>> Register(RegisterDto dto)
        {
            var emailExist = await GetByEmail(dto.Email!);
            if (emailExist != null)
            {
                if(emailExist!.EmailConfirmed)
                    return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"The email you enter {emailExist.Email} is already in use" };
                if(emailExist.Role!=null)
                    _context.Roles.Remove(emailExist.Role!);
                _context.Users.Remove(emailExist);
            }

            var current = DateTime.UtcNow;
            var user = new User
            {
                Id=Guid.NewGuid().ToString(),
                Email = dto.Email!.ToLower(),
                CreatedDate = current,
                ModifyDate = current,
                DeletedDate = null,
                Role=new UserRole { Role=RoleType.Candidate,CreatedDate=current,ModifyDate=current}
            };
            byte[] hash, salt;

            PasswordHasher.GeneratePasswordHasing(dto.Password, out salt, out hash);
            user.PasswordSalt = salt;
            user.PasswordHash = hash;
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
            }
            catch (Exception ex)
            {
                return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured:  {ex.Message}" };

            }
        }

        public async Task<ResponseDto<AuthDto>> VerifyUser(LoginDto dto)
        {
            User? user = await _context.Users
                .Include(a => a.Role)
                .Include(a => a.Candidate)
                .Include(a => a.Company)
                    .ThenInclude(c=>c.CompanyDetail)
                .FirstOrDefaultAsync(a => a.Email == dto.Email!);
            if (user == null) return new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Invalid login attempt." };

            if (!user.EmailConfirmed) return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Unautorize, Message = "Email confirmation required" };
            //if(!user.PhoneConfirmed) return new ResponseDto<AuthDto> {Model=user, Status=ResponseStatus.Unautorize, Message="Phone number confirmation required"};

            try
            {
                var result = await PasswordHasher.VerifyPassword(dto.Password!, user.PasswordSalt!, user.PasswordHash!);
                if (result) return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
                else return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Invalid user name or password." };
            }
            catch (Exception ex)
            {
                return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured: {ex.Message}" };
            }
        }
        public async Task<AuthDto> GetRefreshToken(string refreshToken, HttpContext httpContext)
        {
            return null;
        }
        public async Task<ResponseDto<AuthDto>> ConfirmEmail(ConfirmDto dto)
        {
            var tokens = await _context.UserTokens
                .FirstOrDefaultAsync(a => a.UserId == dto.UserId &&
                    a.Token == dto.Token &&
                    a.TokenType == TokenType.EmailConfirmation);

            if (tokens == null) return new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "Token is notfound." };

            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == dto.UserId);
            if (user == null) return new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "User is notfound." };

            user.EmailConfirmed = true;
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
                return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
            }
            catch (Exception ex)
            {
                return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }
        public async Task<ResponseDto<AuthDto>> GetUser(string id)
        {
            User? user = await _context.Users
               .Include(a => a.Role).FirstOrDefaultAsync(a => a.Id == id);
            if (user == null) return new ResponseDto<AuthDto> { Status = ResponseStatus.NotFound, Message = "User not found." };

            return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
        }
        public async Task<ResponseDto<ConfirmDto>> SendPhoneConfirmation(string userId)
        {
            //Random _random = new Random();
            var number = string.Format("{0:000000}", _random.Next(999999));

            var expiredMinutes = 30;
            var token = await _context.UserTokens
                 .FirstOrDefaultAsync(a => a.UserId == userId &&
                     a.TokenType == TokenType.PhoneConfiration);
            var current = DateTime.UtcNow;
            if (token == null)
            {
                _context.UserTokens.Add(new UserToken
                {
                    UserId = userId,
                    Token = number,
                    TokenType = TokenType.PhoneConfiration,
                    CreatedDate = current,
                    ModifyDate = current,
                    TokenExpireTime = current.AddMinutes(expiredMinutes)
                });
            }
            else
            {
                token.Token = number;
                token.TokenExpireTime = current.AddMinutes(expiredMinutes);
                token.ModifyDate = current;

                _context.UserTokens.Update(token);
            }
            try
            {
                await _context.SaveChangesAsync();
                return new ResponseDto<ConfirmDto> { Model = new ConfirmDto { UserId = userId }, Status = ResponseStatus.Success };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }
        public async Task<ResponseDto<ConfirmDto>> ConfirmPhone(ConfirmDto dto)
        {
            var token = await _context.UserTokens
                 .FirstOrDefaultAsync(a => a.UserId == dto.UserId &&
                     a.Token == dto.Token &&
                     a.TokenType == TokenType.PhoneConfiration);
            var current = DateTime.UtcNow;
            if (token == null)
                return new ResponseDto<ConfirmDto> { Status = ResponseStatus.NotFound, Message = "Token is not found" };

            if (token.TokenExpireTime < current)
                return new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = "Token is expired" };

            var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == dto.UserId);
            if (user == null)
                return new ResponseDto<ConfirmDto> { Status = ResponseStatus.NotFound, Message = "User is not found" };

            user.PhoneConfirmed = true;
            user.ModifyDate = current;
            _context.Users.Update(user);
            try
            {
                await _context.SaveChangesAsync();
                return new ResponseDto<ConfirmDto> { Model = dto, Status = ResponseStatus.Success, Message = "User is not found" };
            }
            catch (Exception ex)
            {
                return new ResponseDto<ConfirmDto> { Status = ResponseStatus.Error, Message = ex.Message };
            }
        }
        public async Task<PagedList<UserDto>> GetUserPagedList(PageParameter pageParameter)
        {
            var users = await _context.Users
                .Include(a => a.Role)
                .Search(pageParameter.SearchTerm!)
                .Sort(pageParameter.OrderBy!)
                .Select(a => (UserDto)a).ToListAsync();
            return PagedList<UserDto>
                .ToPagedList(users, pageParameter.PageNumber, pageParameter.PageSize);
        }
        /// <summary>
        ///     Company registration 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResponseDto<AuthDto>> RegisterCompany(RegisterCompanyDto dto)
        {
            var emailExist = await GetByEmail(dto.Email!);
            if (emailExist != null)
            {
                if (emailExist!.EmailConfirmed)
                    return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"The email you enter {emailExist.Email} is already in use" };
                if (emailExist.Role != null)
                    _context.Roles.Remove(emailExist.Role!);
                _context.Users.Remove(emailExist);
            }

            var current = DateTime.UtcNow;
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = dto.Email!.ToLower(),
                CreatedDate = current,
                ModifyDate = current,
                DeletedDate = null,
                Role = new UserRole { Role = RoleType.Employeer, CreatedDate = current, ModifyDate = current },
                Company=new Company { CompanyTin=dto.CompanyTin,Status=ClientStatus.Pending}
            };
            byte[] hash, salt;

            PasswordHasher.GeneratePasswordHasing(dto.Password!, out salt, out hash);
            user.PasswordSalt = salt;
            user.PasswordHash = hash;
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new ResponseDto<AuthDto> { Model = user, Status = ResponseStatus.Success };
            }
            catch (Exception ex)
            {
                return new ResponseDto<AuthDto> { Status = ResponseStatus.Error, Message = $"Error occured:  {ex.Message}" };

            }
        }
    }
}

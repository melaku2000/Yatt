using Microsoft.EntityFrameworkCore;
using Yatt.Repo.Data;
using Yatt.Repo.Interfaces;
using Yatt.Models.Dtos;
using Yatt.Models.Entities;

namespace Yatt.Repo.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public AppDbContext _context { get; }
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetRefreshToken(string refreshToken, string userAgent)
        {
            string incomeSource = string.IsNullOrEmpty(userAgent) ? "Mobile" : "Web";

            return await _context.RefreshTokens.Where(a => a.Token == refreshToken && a.UserAgent == incomeSource).FirstOrDefaultAsync();
        }

        public async Task<RefreshToken> UpdateRefreshToken(RequestTokenDto dto)
        {
            var expireHour = 7;
            string incomeSource = string.IsNullOrEmpty(dto.UserAgent) ? "Mobile" : "Web";

            var refToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(a => a.UserId == dto.UserId && a.UserAgent == incomeSource);

            var current = DateTime.UtcNow;
            var token = Guid.NewGuid().ToString();
            if (refToken == null)
            {
                refToken = new RefreshToken
                {
                    Id=Guid.NewGuid().ToString(),
                    UserId = dto.UserId,
                    UserAgent = incomeSource,
                    Token = token,
                    IpAddress = dto.IpAddress,
                    TokenExpireTime = current.AddHours(expireHour),
                    CreatedDate = current,
                    ModifyDate = current,
                    DeletedDate = null
                };
                _context.RefreshTokens.Add(refToken);
            }
            else
            {
                refToken.IpAddress = dto.IpAddress;
                refToken.Token = token;
                refToken.ModifyDate = current;

                _context.RefreshTokens.Update(refToken);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return refToken;
        }
    }
}

﻿using System.Security.Claims;
using Yatt.Models.Dtos;

namespace Yatt.Api.Handlers
{
    public interface ITokenManager
    {
        Task<string> GenerateToken(AuthDto user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

namespace Selfra_Services.Infrastructure
{
    public class Authentication
    {
    //    public static async Task<TokenResponse> CreateToken(ApplicationUser? user, string role, JwtSettings jwtSettings, IUnitOfWork unitOfWork, bool isRefresh = false, string ipAddress = null!)
    //    {
    //        // Tạo ra các claims
    //        DateTime now = DateTime.UtcNow;

    //        // Danh sách các claims chung cho cả Access Token và Refresh Token
    //        List<Claim> claims = new List<Claim>
    //            {
    //                new Claim("id", user!.Id.ToString()),
    //                new Claim("role", role)
    //            };

    //        // đăng kí khóa bảo mật
    //        SymmetricSecurityKey? key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey ?? string.Empty));
    //        SigningCredentials? creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    //        // Generate access token
    //        DateTime dateTimeAccessExpr = now.AddMinutes(jwtSettings.AccessTokenExpirationMinutes);
    //        claims.Add(new Claim("token_type", "access"));
    //        JwtSecurityToken accessToken = new JwtSecurityToken(
    //            claims: claims,
    //            issuer: jwtSettings.Issuer,
    //            audience: jwtSettings.Audience,
    //            expires: dateTimeAccessExpr,
    //            signingCredentials: creds
    //        );

    //        string refreshTokenString = string.Empty;
    //        string accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);

    //        if (isRefresh == false)
    //        {
    //            // tạo ra refresh Token
    //            DateTime datetimeRefrestExpr = now.AddDays(jwtSettings.RefreshTokenExpirationDays);

    //            claims.Remove(claims.First(c => c.Type == "token_type"));
    //            claims.Add(new Claim("token_type", "refresh"));

    //            JwtSecurityToken? refreshToken = new JwtSecurityToken(
    //                claims: claims,
    //                issuer: jwtSettings.Issuer,
    //                audience: jwtSettings.Audience,
    //                expires: datetimeRefrestExpr,
    //                signingCredentials: creds
    //            );

    //            refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);
    //        }

    //        return new TokenResponse
    //        {
    //            AccessToken = accessTokenString,
    //            RefreshToken = refreshTokenString,
    //            User = new ResponseUserModel
    //            {
    //                Id = user.Id.ToString(),
    //                Username = user.UserName,
    //                Email = user.Email,
    //                FullName = user.FullName,
    //                PhoneNumber = user.PhoneNumber,
    //                CreatedTime = user.CreatedTime,
    //                Role = role.ToString(),
    //            }
    //        };
    //    }
    }
}

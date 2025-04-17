using champsModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace champsProjectServer;
public class JwtHandler(IConfiguration configuration, UserManager<ChampsUser> userManager)
{
    public async Task<JwtSecurityToken> GetTokenAsync(ChampsUser user) =>
        new(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: await GetClaimsAsync(user),
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(configuration["JwtSettings:ExpirationTimeInMinutes"])),
            signingCredentials: GetSigningCredentials());

    private SigningCredentials GetSigningCredentials()
    {
        byte[] key = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecurityKey"]!);
        SymmetricSecurityKey secret = new(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaimsAsync(ChampsUser user)
    {
        List<Claim> claims = [new Claim(ClaimTypes.Name, user.UserName!)];
        claims.AddRange(from role in await userManager.GetRolesAsync(user) select new Claim(ClaimTypes.Role, role));
        return claims;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApp.Data.Settings;
using WebApp.Models;

namespace WebApp.Services;

public static class TokenService
{
    public static string GenerateToken(User usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Secret));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                new Claim("Id", usuario.Id),
                new Claim(ClaimTypes.Name, usuario.Name),
                new Claim(ClaimTypes.Role, usuario.Perfis.Descricao)
             }),
            Expires = DateTime.Now.AddHours(8),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static string EncrypPassword(string password)
    {
        string encryptedString = "";
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(Settings.SerectPass);
            aesAlg.Mode = CipherMode.ECB;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor();

            byte[] originalBytes = Encoding.UTF8.GetBytes(password);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(originalBytes, 0, originalBytes.Length);

            encryptedString = Convert.ToBase64String(encryptedBytes);
        }

        return encryptedString;
    }
}

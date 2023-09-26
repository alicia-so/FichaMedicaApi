using WebApp.Models;
using WebApp.Services;

public class TokenServiceTests
{
    [Fact]
    public void GenerateToken_ReturnsValidToken()
    {
        User user = new User
        {
            Id = "1234",
            Name = "John Doe",
            Email = "fafjafja@email.com",
            LastName = "Doe",
            Password = "password123",
            PhoneNumber = "123456789",
            PerfilId = "1234",
            Perfis = new Perfil
            {
                Descricao = "Medico"
            }
        };

        string token = TokenService.GenerateToken(user);

        Assert.NotNull(token);
        Assert.IsType<string>(token);
        Assert.NotEmpty(token);
    }

    [Fact]
    public void EncrypPassword_ReturnsEncryptedPassword()
    {
        string password = "password123";

        string encryptedPassword = TokenService.EncrypPassword(password);

        Assert.NotNull(encryptedPassword);
        Assert.IsType<string>(encryptedPassword);
        Assert.NotEmpty(encryptedPassword);
        Assert.NotEqual(password, encryptedPassword);
    }
}
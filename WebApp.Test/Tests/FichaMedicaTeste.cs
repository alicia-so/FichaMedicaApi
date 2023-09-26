using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApp.Controllers;
using WebApp.Data.Context;
using WebApp.Data.Dtos.FichaMedica;
using WebApp.Data.Dtos.User;
using WebApp.Models;

public class FichaMedicaControllerTests
{
    private readonly FichaMedicaController _controller;
    private readonly SistemaContext _context;
    private readonly IMapper _mapper;

    public FichaMedicaControllerTests()
    {
        _context = new SistemaContext();
        _controller = new FichaMedicaController(_mapper, _context);
    }

    [Fact]
    public void Test_GetAll_ShouldReturn_ValidResult()
    {
        int skip = 0;
        int take = 50;

        // Arrange
        var usuarios = new List<User>
            {
                new User{ Id = "1", FullName = "Joe Doe", PhoneNumber = "123456789", Email= "afaf@gmail.com", Name= "Joe", LastName= "Doe", Password="123da@", PerfilId="2"},
                new User{ Id = "2", FullName = "Zeca Doe", PhoneNumber = "987654321",Email= "sfafadf@gmail.com", Name= "Zeca", LastName= "Doe", Password="123wdwa@", PerfilId="3"}
            };
        _context.Users.AddRange(usuarios);
        _context.SaveChanges();

        // Act
        var result = _controller.GetAll(skip, take);
        var okResult = result as OkObjectResult;

        // Assert            
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        var actualUsers = Assert.IsAssignableFrom<List<ReadUserDto>>(okResult.Value);
        Assert.NotEmpty(actualUsers);
    }

    [Fact]
    public void Test_GetByIdPaciente_WithValidUserCredentials_ShouldReturn_ReadFichaMedicaDto()
    {
        // Arrange
        var fakeUserId = "1";
        var fakeUserClaim = new Mock<System.Security.Claims.Claim>();
        fakeUserClaim.Setup(x => x.Type).Returns("Id");
        fakeUserClaim.Setup(x => x.Value).Returns(fakeUserId);

        var fakeUser = new Mock<System.Security.Claims.ClaimsPrincipal>();
        fakeUser.Setup(x => x.Claims).Returns(new List<System.Security.Claims.Claim> { fakeUserClaim.Object });

        var IdPaciente = fakeUserId;
        var ficha = new FichaMedica { Id = "1", PacienteId = fakeUserId, Descricao = "Ficha médica teste", MedicoId= fakeUserId };
        _context.Fichas.Add(ficha);
        _context.SaveChanges();

        // Act
        var result = _controller.RecuperarPorIdPaciente(IdPaciente);
        var okResult = result;

        // Assert            
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult);
        var actualFicha = Assert.IsAssignableFrom<ReadFichaMedicaDto>(okResult.Value);
        Assert.NotNull(actualFicha);
    }

    [Fact]
    public void Test_GetByIdPaciente_WithInvalidUserCredentials_ShouldReturn_NotFound()
    {
        //Arrange
        var fakeUserId = "100";
        var fakeUserClaim = new Mock<System.Security.Claims.Claim>();
        fakeUserClaim.Setup(x => x.Type).Returns("Id");
        fakeUserClaim.Setup(x => x.Value).Returns(fakeUserId);

        var fakeUser = new Mock<System.Security.Claims.ClaimsPrincipal>();
        fakeUser.Setup(x => x.Claims).Returns(new List<System.Security.Claims.Claim> { fakeUserClaim.Object });

        var IdPaciente = "1";
        var ficha = new FichaMedica { Id = "1", PacienteId = IdPaciente, Descricao = "Ficha médica teste", MedicoId="2" };
        _context.Fichas.Add(ficha);
        _context.SaveChanges();

        //Act
        var result = _controller.RecuperarPorIdPaciente(IdPaciente);
        var notFoundResult = result;

        //Assert
        Assert.NotNull(notFoundResult);
        Assert.Equal(404, notFoundResult);
        var message = Assert.IsAssignableFrom<string>(notFoundResult.Value);
        Assert.Equal("Paciente logado, não pode consultar fichas de outros pacientes.", message);
    }

    [Fact]
    public void Test_CreateFicha_ShouldReturn_CreatedAtActionResult()
    {
        // Arrange
        var createDto = new CreateFichaMedicaDto { PacienteId = "1", Descricao = "Ficha médica teste", MedicoId="2" };

        // Act
        var result = _controller.CreateFicha(createDto);
        var okResult = result as CreatedAtActionResult;

        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(201, okResult.StatusCode);
        Assert.IsAssignableFrom<ReadFichaMedicaDto>(okResult.Value);
    }

    [Fact]
    public void Test_DeleteFicha_ShouldDeleteProperly()
    {
        // Arrange
        var ficha = new FichaMedica { Id = "1", PacienteId = "1", Descricao = "Ficha médica teste", MedicoId="2"};
        _context.Fichas.Add(ficha);
        _context.SaveChanges();

        // Act
        var result = _controller.DeleteFichaMedica(ficha.Id.ToString());
        var okResult = result as OkObjectResult;

        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsAssignableFrom<string>(okResult.Value);

        var deletedFicha = _context.Fichas.Find(ficha.Id);
        Assert.Null(deletedFicha);
    }

    [Fact]
    public void Test_EditFicha_ShouldReturn_Ok()
    {
        // Arrange
        var ficha = new FichaMedica { Id = "1", PacienteId = "1", Descricao = "Ficha médica teste", MedicoId="2" };
        _context.Fichas.Add(ficha);
        _context.SaveChanges();

        var editDto = new EditFichaMedicaDto { Descricao = "Ficha médica alterada" };

        // Act
        var result = _controller.EditFicha(ficha.Id.ToString(), editDto);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsAssignableFrom<string>(okResult.Value);

        var updatedFicha = _context.Fichas.Find(ficha.Id);
        Assert.NotNull(updatedFicha);
        Assert.Equal(editDto.Descricao, updatedFicha.Descricao);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Context;
using WebApp.Data.Dtos.User;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly SistemaContext _context;
    public UserController(IMapper mapper, SistemaContext context){
        _mapper = mapper;
        _context = context;
    }
    

    [HttpPost]
    [Route("Create")]
    public IActionResult CreateUsuario([FromBody]CreateUserDto dto)
    {
        User usuario = _mapper.Map<User>(dto);
        usuario.FullName = dto.Name + " " + dto.LastName;
        usuario.Password = TokenService.EncrypPassword(dto.Password);

        _context.Users.Add(usuario);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarUsuarioPorId), new { id = usuario.Id}, dto);
    }

    
    [HttpGet("{id}")]
    public ReadUserDto RecuperarUsuarioPorId([FromQuery]string Id)
    {
        var usuario = _context.Users.FirstOrDefault(p => p.Id == Id);
        return _mapper.Map<ReadUserDto>(usuario);
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "Medico")]
    public IEnumerable<ReadUserDto> GetAll([FromQuery]int skyp=0, [FromQuery]int take=50)
    {
        var usuarios = _context.Users.Skip(skyp).Take(take).ToList();
        return _mapper.Map<List<ReadUserDto>>(usuarios);
    }

    [HttpPost]
    [Route("login")]
    public ActionResult<dynamic> Autenticar([FromBody]LoginUserDto dto)
    {
        var encryptedPassword = TokenService.EncrypPassword(dto.Password);
        var email = dto.Email.ToUpper();
        var user = _context.Users.FirstOrDefault(x => x.Email.ToUpper() == email && x.Password == encryptedPassword);
        
        if(user == null){
            return NotFound(new { message = "Usuario não encontrado "});
        }
        ReadUserDto retorno = _mapper.Map<ReadUserDto>(user);
        retorno.FullName = user.Name+ " "+user.LastName;
        var token = TokenService.GenerateToken(user);
        return new{
            User = retorno,
            Token = token
        };
    }
}

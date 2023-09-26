using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Data.Context;
using WebApp.Data.Dtos.FichaMedica;
using WebApp.Data.Dtos.User;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class FichaMedicaController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly SistemaContext _context;
    public FichaMedicaController(IMapper mapper, SistemaContext context){
        _mapper = mapper;
        _context = context;
    }
    

    [HttpPost]
    [Route("Create")]
    [Authorize(Roles = "Medico")]
    public IActionResult CreateFicha([FromBody]CreateFichaMedicaDto dto)
    {
        var ficha = _mapper.Map<FichaMedica>(dto);
        
        _context.Fichas.Add(ficha);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPorId), new { id = ficha.Id}, dto);
    }

    
    [HttpGet]
    [Route("GetById")]
    [Authorize(Roles = "Medico")]
    public ReadFichaMedicaDto RecuperarPorId([FromQuery]string Id)
    {
        var ficha = _context.Fichas.FirstOrDefault(p => p.Id.Equals(Id));
        return _mapper.Map<ReadFichaMedicaDto>(ficha);
    }

    [HttpGet]
    [Route("GetByIdPaciente")]
    [Authorize]
    public ActionResult<dynamic> RecuperarPorIdPaciente([FromQuery] string IdPaciente)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
        string userId = "";
        if (userIdClaim != null)
        {
            userId = userIdClaim.Value;
        }
        if (!userId.Equals(IdPaciente))
            return NotFound(new { message = "Paciente logado, não pode consultar fichas de outros pacientes." });

        var ficha = _context.Fichas.FirstOrDefault(p => p.PacienteId.Equals(IdPaciente));
        return _mapper.Map<ReadFichaMedicaDto>(ficha);
    }



    [HttpGet("GetAll")]
    [Authorize(Roles = "Medico")]
    public IEnumerable<ReadUserDto> GetAll([FromQuery]int skyp=0, [FromQuery]int take=50)
    {
        var usuarios = _context.Users.Skip(skyp).Take(take).ToList();
        return _mapper.Map<List<ReadUserDto>>(usuarios);
    }

    [HttpDelete("DeleteFicha")]
    [Authorize(Roles = "Medico")]
    public IActionResult DeleteFichaMedica([FromQuery] string Id)
    {
        var ficha = _context.Fichas.FirstOrDefault(p => p.Id.Equals(Id));

        if (ficha == null)
        {
            return NotFound(new { message = "Ficha médica não encontrada." });
        }

        _context.Fichas.Remove(ficha);
        _context.SaveChanges();

        return Ok(new { message = "Ficha médica deletada com sucesso." });
    }

    [HttpPut("EditFicha")]
    [Authorize(Roles = "Medico")]
    public IActionResult EditFicha([FromQuery] string Id, [FromBody] EditFichaMedicaDto dto)
    {
        var ficha = _context.Fichas.FirstOrDefault(f => f.Id.Equals(Id));

        if (ficha == null)
        {
            return NotFound(new { message = "Ficha médica não encontrada." });
        }

        if (!string.IsNullOrWhiteSpace(dto.Descricao))
        {
            ficha.Descricao = dto.Descricao;
        }

        if (!string.IsNullOrWhiteSpace(dto.FullName))
        {
            var usuario = _context.Users.FirstOrDefault(u => u.Id.Equals(ficha.PacienteId));

            if (usuario != null)
            {
                usuario.FullName = dto.FullName;
            }
        }

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber))
        {
            var usuario = _context.Users.FirstOrDefault(u => u.Id.Equals(ficha.PacienteId));

            if (usuario != null)
            {
                usuario.PhoneNumber = dto.PhoneNumber;
            }
        }

        _context.SaveChanges();

        return Ok(new { message = "Ficha médica atualizada com sucesso." });
    }


}

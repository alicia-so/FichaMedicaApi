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
    public FichaMedicaController(IMapper mapper, SistemaContext context)
    {
        _mapper = mapper;
        _context = context;
    }


    [HttpPost]
    [Route("Create")]
    [Authorize(Roles = "Medico")]
    public IActionResult CreateFicha([FromBody] CreateFichaMedicaDto dto)
    {
        var ficha = _mapper.Map<FichaMedica>(dto);

        _context.Fichas.Add(ficha);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPorId), new { id = ficha.Id }, dto);
    }


    [HttpGet]
    [Route("GetById")]
    [Authorize(Roles = "Medico")]
    public ReadFichaMedicaDto RecuperarPorId([FromQuery] string Id)
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
            return NotFound(new { message = "Paciente logado, n�o pode consultar fichas de outros pacientes." });

        var ficha = _context.Fichas.FirstOrDefault(p => p.PacienteId.Equals(IdPaciente));
        return _mapper.Map<ReadFichaMedicaDto>(ficha);
    }

    [HttpGet]
    [Route("GetAllFromPaciente")]
    [Authorize]
    public ActionResult<dynamic> GetAllFromPaciente([FromQuery] string Id)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
        string userId = "";
        if (userIdClaim != null)
        {
            userId = userIdClaim.Value;
        }
        if (!userId.Equals(Id))
            return NotFound(new { message = "Paciente logado, n�o pode consultar fichas de outros pacientes." });

        var fichas = _context.Fichas.Where(p => p.PacienteId.Equals(Id)).ToList();
        return _mapper.Map<List<ReadFichaMedicaDto>>(fichas);
    }

    [HttpGet("GetAll")]
    [Authorize(Roles = "Medico")]
    public IEnumerable<ReadFichaMedicaDto> GetAll([FromQuery] int skyp = 0, [FromQuery] int take = 50)
    {
        var fichas = _context.Fichas.Skip(skyp).Take(take).ToList();
        return _mapper.Map<List<ReadFichaMedicaDto>>(fichas);
    }

    [HttpDelete("DeleteFicha")]
    [Authorize(Roles = "Medico")]
    public IActionResult DeleteFichaMedica([FromQuery] string Id)
    {
        var ficha = _context.Fichas.FirstOrDefault(p => p.Id.Equals(Id));

        if (ficha == null)
        {
            return NotFound(new { message = "Ficha m�dica n�o encontrada." });
        }

        _context.Fichas.Remove(ficha);
        _context.SaveChanges();

        return Ok(new { message = "Ficha m�dica deletada com sucesso." });
    }

    [HttpPut("EditFicha")]
    [Authorize(Roles = "Medico")]
    public IActionResult EditFicha([FromQuery] string Id, [FromBody] EditFichaMedicaDto dto)
    {
        var ficha = _context.Fichas.FirstOrDefault(f => f.Id.Equals(Id));

        if (ficha == null)
        {
            return NotFound(new { message = "Ficha m�dica n�o encontrada." });
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

        return Ok(new { message = "Ficha m�dica atualizada com sucesso." });
    }


}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Context;
using WebApp.Data.Dtos.Perfil;
using WebApp.Models;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class PerfilController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly SistemaContext _context;

    public PerfilController(IMapper mapper,SistemaContext context){
        _mapper = mapper;
        _context = context;
    }
    

    [HttpPost]
    [Route("Create")]
    public IActionResult CreatePerfil([FromBody]CreatePerfilDto dto)
    {
        var perfil = _mapper.Map<Perfil>(dto);
        _context.Add(perfil);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperarPerfilPorId), new { id = perfil.Id}, perfil);
    }

    [HttpGet("{Id}")]
    public IActionResult RecuperarPerfilPorId([FromQuery]string Id)
    {
        var perfil = _context.Perfis.FirstOrDefault(p => p.Id == Id);
        if(perfil == null)
            return NotFound();
        return Ok(_mapper.Map<ReadPerfilDto>(perfil));
    }

    [HttpGet]
    [Route("GetAll")]
    public IEnumerable<ReadPerfilDto> RecuperarPerfis([FromQuery]int skyp=0, [FromQuery]int take=50)
    {
        return _mapper.Map<List<ReadPerfilDto>>(_context.Perfis.Skip(skyp).Take(take).ToList());
    }
    
}

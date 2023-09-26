using AutoMapper;
using WebApp.Data.Dtos.FichaMedica;
using WebApp.Models;

namespace WebApp.Profiles;

public class FichaMedicaProfile : Profile{
    
    public FichaMedicaProfile(){
        CreateMap<CreateFichaMedicaDto, FichaMedica>();
        CreateMap<FichaMedica, ReadFichaMedicaDto>()
        .ForMember(fichaDto => fichaDto.Paciente, opt => opt.MapFrom(ficha => ficha.Paciente))
        .ForMember(fichaDto => fichaDto.Medico, opt => opt.MapFrom(ficha => ficha.Medico));
    }
}
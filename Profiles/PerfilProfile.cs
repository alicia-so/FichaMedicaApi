using AutoMapper;
using WebApp.Data.Dtos.Perfil;
using WebApp.Models;

namespace WebApp.Profiles;
public class PerfilProfile : Profile
{
    public PerfilProfile(){
        CreateMap<CreatePerfilDto, Perfil>();
        CreateMap<ReadPerfilDto, Perfil>();

        CreateMap<Perfil, CreatePerfilDto>();
        CreateMap<Perfil, ReadPerfilDto>();
    }
    
}

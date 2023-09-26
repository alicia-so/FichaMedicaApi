using AutoMapper;
using WebApp.Data.Dtos.User;
using WebApp.Models;

namespace WebApp.Profiles;

public class UserProfile : Profile{
    
    public UserProfile(){
        CreateMap<CreateUserDto, User>();
        CreateMap<ReadUserDto, User>()
        .ForMember(usuario => usuario.Perfis,opt => opt.MapFrom(dto => dto.Perfis));

        CreateMap<User, CreateUserDto>();
        CreateMap<User, ReadUserDto>()
        .ForMember(dto => dto.Perfis,opt => opt.MapFrom(usuario => usuario.Perfis));
    }
}
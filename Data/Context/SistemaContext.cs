using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data.Context;
public class SistemaContext : DbContext
{
    public SistemaContext()
    {
    }

    public SistemaContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Perfil> Perfis {get; set;}
    public DbSet<FichaMedica> Fichas {get; set;}

}

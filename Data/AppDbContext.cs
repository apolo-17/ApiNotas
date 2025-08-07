using Microsoft.EntityFrameworkCore;
using ApiNotas.Models;
using ApiNotas.Auth;

namespace ApiNotas.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Nota> Notas => Set<Nota>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
}

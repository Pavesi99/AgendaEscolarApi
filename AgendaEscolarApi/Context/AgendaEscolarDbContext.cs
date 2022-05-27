using Domain;
using Microsoft.EntityFrameworkCore;

namespace Context;
public class AgendaEscolarDbContext : DbContext
{
    public AgendaEscolarDbContext(DbContextOptions<AgendaEscolarDbContext> options)
     : base(options) { }

    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Materia> Materias => Set<Materia>();
    public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Professor>().ToTable("Professor").HasKey(m => m.Id);
        builder.Entity<Materia>().ToTable("Materia").HasKey(m => m.Id);

        base.OnModelCreating(builder);
    }

}
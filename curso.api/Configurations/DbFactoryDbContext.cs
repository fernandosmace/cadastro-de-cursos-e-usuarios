using curso.api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace curso.api.Configurations
{
  public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
  {
    public CursoDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<CursoDbContext>();
      optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;");
      CursoDbContext contexto = new CursoDbContext(optionsBuilder.Options);

      return contexto;
    }
  }
}
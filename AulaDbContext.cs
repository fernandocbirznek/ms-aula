using Microsoft.EntityFrameworkCore;
using ms_aula.Domains;
using System.Data.Common;

namespace ms_aula
{
    public class AulaDbContext : DbContext, IDbContext
    {
        public AulaDbContext(DbContextOptions<AulaDbContext> options) : base(options) { }
        public DbSet<AreaFisica> AreaFisica { get; set; }
        public DbSet<AreaFisicaDivisao> AreaFisicaDivisao { get; set; }
        public DbSet<ArquivoPdf> ArquivoPdf { get; set; }
        public DbSet<Aula> Aula { get; set; }
        public DbSet<AulaComentario> AulaComentario { get; set; }
        public DbSet<AulaFavoritada> AulaFavoritada { get; set; }
        public DbSet<AulaSessao> AulaSessao { get; set; }
        public DbSet<AulaSessaoFavoritada> AulaSessaoFavoritada { get; set; }
        public DbSet<AulaTag> AulaTag { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<UsuarioAulaCurtido> UsuarioAulaCurtido { get; set; }
        public DbSet<WidgetConcluido> WidgetConcluido { get; set; }
        public DbSet<WidgetCursando> WidgetCursando { get; set; }
        public DbSet<WidgetCursar> WidgetCursar { get; set; }

        public DbConnection Connection => base.Database.GetDbConnection();
    }
}

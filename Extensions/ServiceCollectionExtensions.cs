using Microsoft.EntityFrameworkCore;
using ms_aula.Domains;
using ms_aula.Interface;
using ms_aula.Repositories;

namespace ms_aula.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void SetupRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<AreaFisica>), typeof(Repository<AreaFisica>));
            services.AddScoped(typeof(IRepository<Aula>), typeof(Repository<Aula>));
            services.AddScoped(typeof(IRepository<AulaComentario>), typeof(Repository<AulaComentario>));
            services.AddScoped(typeof(IRepository<AulaFavoritada>), typeof(Repository<AulaFavoritada>));
            services.AddScoped(typeof(IRepository<AulaSessao>), typeof(Repository<AulaSessao>));
            services.AddScoped(typeof(IRepository<AulaSessaoFavoritada>), typeof(Repository<AulaSessaoFavoritada>));
            services.AddScoped(typeof(IRepository<WidgetConcluido>), typeof(Repository<WidgetConcluido>));
            services.AddScoped(typeof(IRepository<WidgetCursando>), typeof(Repository<WidgetCursando>));
            services.AddScoped(typeof(IRepository<WidgetCursar>), typeof(Repository<WidgetCursar>));
        }

        public static void SetupDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AulaDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(AulaDbContext).Assembly.FullName)),
                ServiceLifetime.Transient, ServiceLifetime.Transient
                );
        }
    }
}

using API.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace API.IoC
{
    public static class InjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<Uow.IUnitOfWork, Uow.UnitOfWork>();
            services.AddScoped<MarcaRepository>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<CondicaoRepository>();
            services.AddScoped<TecidoRepository>();
            services.AddScoped<ProdutoRepository>();
            services.AddScoped<ClienteRepository>();
            services.AddScoped<MangaRepository>();
        }
    }
}

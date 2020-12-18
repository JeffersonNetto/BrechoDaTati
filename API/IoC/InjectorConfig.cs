using API.Repositories;
using API.Validators;
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
            services.AddScoped<ClienteValidator>();
            services.AddMemoryCache();
        }
    }
}

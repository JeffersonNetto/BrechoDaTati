using API.Models;
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
            services.AddScoped<IRepositoryBase<Marca>, MarcaRepository>();
            services.AddScoped<IRepositoryBase<Categoria>, CategoriaRepository>();
            services.AddScoped<IRepositoryBase<Condicao>, CondicaoRepository>();
            services.AddScoped<IRepositoryBase<Tecido>, TecidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IRepositoryBase<Manga>, MangaRepository>();
            services.AddScoped<ClienteValidator>();
            services.AddMemoryCache();
        }
    }
}

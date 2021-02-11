using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace API.IoC
{
    public static class InjectorConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositoryBase<Marca>, MarcaRepository>();
            services.AddScoped<IRepositoryBase<Categoria>, CategoriaRepository>();
            services.AddScoped<IRepositoryBase<Condicao>, CondicaoRepository>();
            services.AddScoped<IRepositoryBase<Tecido>, TecidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IRepositoryBase<Manga>, MangaRepository>();
            services.AddScoped<ICupomRepository, CupomRepository>();
            services.AddScoped<ClienteValidator>();
            services.AddScoped<ClienteEnderecoValidator>();
            services.AddMemoryCache();
        }
    }
}

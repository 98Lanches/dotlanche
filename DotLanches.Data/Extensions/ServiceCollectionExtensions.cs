using DotLanches.DataMongo.Data;
using DotLanches.Domain.Interfaces.Repositories;
using DotLanches.DataMongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using DotLanches.Domain.Entities;

namespace DotLanches.DataMongo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string DATABASE_NAME = "dotlanche";

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(provider => new MongoClient(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(provider => provider.GetRequiredService<MongoClient>().GetDatabase(DATABASE_NAME));

            // Create a singleton instance of the IMongoCollection<Produto>
            //services.AddSingleton<IMongoCollection<Produto>>(provider => provider.GetRequiredService<IMongoDatabase>().GetCollection<Produto>("ProdutoTeste"));

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            return services;
        }
    }
}

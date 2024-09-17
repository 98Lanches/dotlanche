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
        //const string conn = "mongodb://localhost:27017/dotlanches";
        const string conn = "mongodb+srv://dotlanche:dotlanche@98lanches.ojp4b.mongodb.net/?retryWrites=true&w=majority&appName=98Lanches";

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Create a singleton instance of the MongoClient
            services.AddSingleton<MongoClient>(provider => new MongoClient(conn));

            // Create a singleton instance of the IMongoDatabase
            services.AddSingleton<IMongoDatabase>(provider => provider.GetRequiredService<MongoClient>().GetDatabase("dotlanche"));

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

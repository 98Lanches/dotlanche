using DotLanches.Domain.Entities;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Data
{
    public class DotLanchesDbContext
    {
        private readonly IMongoDatabase _database;

        public DotLanchesDbContext(IMongoClient mongoClient, string databaseName)
        {
            _database = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<Produto> Produtos => _database.GetCollection<Produto>("Produtos");
        public IMongoCollection<Cliente> Clientes => _database.GetCollection<Cliente>("Clientes");
        public IMongoCollection<Pagamento> Pagamentos => _database.GetCollection<Pagamento>("Pagamento");
        public IMongoCollection<Pedido> Pedidos => _database.GetCollection<Pedido>("Pedidos");
    }
}

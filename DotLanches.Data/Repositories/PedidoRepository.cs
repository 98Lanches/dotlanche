using DotLanches.DataMongo.Exceptions;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Repositories
{
    internal class PedidoRepository : IPedidoRepository
    {
        private readonly IMongoCollection<Pedido> _pedidosCollection;
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public PedidoRepository(IMongoDatabase database)
        {
            _pedidosCollection = database.GetCollection<Pedido>("Pedidos");
            _clientesCollection = database.GetCollection<Cliente>("Clientes");
        }

        public async Task Add(Pedido pedido)
        {
            // Ensure that Cliente exists
            if (pedido.ClienteCpf is not null)
            {
                var cliente = await _clientesCollection
                    .Find(c => c.Cpf.Number == pedido.ClienteCpf)
                    .FirstOrDefaultAsync();

                if (cliente == null)
                    throw new EntityNotFoundException("Cliente not found!");

                pedido.ClienteCpf = cliente.Cpf.Number;
            }

            await _pedidosCollection.InsertOneAsync(pedido);
        }

        public async Task<IEnumerable<Pedido>> GetPedidosQueue()
        {
            var queueStatusIds = new[] { EStatus.pronto, EStatus.emPreparo, EStatus.recebido };

            var filter = Builders<Pedido>.Filter.In(p => p.Status, queueStatusIds);
            var sort = Builders<Pedido>.Sort.Descending(p => p.Status).Ascending(p => p.CreatedAt);

            return await _pedidosCollection
                .Find(filter)
                .Sort(sort)
                .ToListAsync();
        }

        public async Task<int> GetLastQueueKeyNumber()
        {
            var lastQueueKey = await _pedidosCollection
                .Find(_ => true)
                .SortByDescending(p => p.QueueKey)
                .Limit(1)
                .Project(p => p.QueueKey)
                .FirstOrDefaultAsync();

            return lastQueueKey;
        }

        public async Task<Pedido?> GetById(Guid id)
        {
            return await _pedidosCollection
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Pedido> Update(Pedido pedido)
        {
            var filter = Builders<Pedido>.Filter.Eq(p => p.Id, pedido.Id);
            var result = await _pedidosCollection.ReplaceOneAsync(filter, pedido);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return pedido;
        }

        public async Task<Pedido> UpdateStatus(Pedido pedido)
        {
            var filter = Builders<Pedido>.Filter.Eq(p => p.Id, pedido.Id);
            var update = Builders<Pedido>.Update.Set(p => p.Status, pedido.Status);

            var result = await _pedidosCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return pedido;
        }
    }
}

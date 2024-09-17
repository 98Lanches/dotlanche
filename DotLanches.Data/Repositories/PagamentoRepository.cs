using DotLanches.DataMongo.Exceptions;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Repositories
{
    internal class PagamentoRepository : IPagamentoRepository
    {
        private readonly IMongoCollection<Pagamento> _pagamentosCollection;
        private readonly IMongoCollection<Pedido> _pedidosCollection;

        public PagamentoRepository(IMongoDatabase database)
        {
            _pagamentosCollection = database.GetCollection<Pagamento>("Pagamentos");
            _pedidosCollection = database.GetCollection<Pedido>("Pedidos");
        }

        public async Task Add(Pagamento pagamento)
        {
            await _pagamentosCollection.InsertOneAsync(pagamento);
        }

        public async Task<Pagamento> Update(Pagamento pagamento)
        {
            var filter = Builders<Pagamento>.Filter.Eq(p => p.Id, pagamento.Id);
            var result = await _pagamentosCollection.ReplaceOneAsync(filter, pagamento);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return pagamento;
        }

        public async Task<Pagamento> GetByIdPedido(Guid idPedido)
        {
            // Find the pedido by id
            var pedido = await _pedidosCollection
                .Find(p => p.Id == idPedido)
                .FirstOrDefaultAsync();

            if (pedido is null)
                throw new EntityNotFoundException();

            // Fetch and return the associated pagamento
            var pagamento = await _pagamentosCollection
                .Find(p => p.Id == pedido.Pagamento.Id) // Assumes PagamentoId is used to reference Pagamento
                .FirstOrDefaultAsync();

            if (pagamento is null)
                throw new EntityNotFoundException();

            return pagamento;
        }
    }
}

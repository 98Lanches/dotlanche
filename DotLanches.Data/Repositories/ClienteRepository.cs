using DotLanches.DataMongo.Exceptions;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Repositories
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClienteRepository(IMongoDatabase database)
        {
            _clientesCollection = database.GetCollection<Cliente>("Clientes");
        }

        public async Task Add(Cliente cliente)
        {
            await _clientesCollection.InsertOneAsync(cliente);
        }

        public async Task<Cliente> Edit(Cliente cliente)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Cpf.Number, cliente.Cpf.Number);
            var result = await _clientesCollection.ReplaceOneAsync(filter, cliente);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return cliente;
        }

        public async Task<Cliente> Delete(string cpf)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Cpf.Number, cpf);
            var cliente = await _clientesCollection.FindOneAndDeleteAsync(filter);

            if (cliente == null)
                throw new EntityNotFoundException();

            return cliente;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            return await _clientesCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Cliente?> GetByCpf(string cpf)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Cpf.Number, cpf);
            return await _clientesCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetById(string cpf)
        {
            var filter = Builders<Cliente>.Filter.Eq(c => c.Cpf.Number, cpf);
            var cliente = await _clientesCollection.Find(filter).FirstOrDefaultAsync();

            if (cliente == null)
                throw new EntityNotFoundException();

            return cliente;
        }
    }
}

using DotLanches.DataMongo.Exceptions;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using MongoDB.Driver;

namespace DotLanches.DataMongo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IMongoCollection<Produto> _produtosCollection;

        public ProdutoRepository(IMongoDatabase database)
        {
            _produtosCollection = database.GetCollection<Produto>("Produtos");
        }

        public async Task Add(Produto produto)
        {
            await _produtosCollection.InsertOneAsync(produto);
        }

        public async Task<Produto> Edit(Produto produto)
        {
            var filter = Builders<Produto>.Filter.Eq(p => p.Id, produto.Id);
            var update = Builders<Produto>.Update
                .Set(p => p.Name, produto.Name)
                .Set(p => p.Description, produto.Description)
                .Set(p => p.Price, produto.Price)
                .Set(p => p.Categoria, produto.Categoria);

            var result = await _produtosCollection.UpdateOneAsync(filter, update);

            if (result.MatchedCount == 0)
                throw new EntityNotFoundException();

            return produto;
        }

        public async Task<Produto> Delete(Guid idProduto)
        {
            var filter = Builders<Produto>.Filter.Eq(p => p.Id, idProduto);
            var result = await _produtosCollection.FindOneAndDeleteAsync(filter);

            if (result == null)
                throw new EntityNotFoundException();

            return result;
        }

        public async Task<IEnumerable<Produto>> GetByCategoria(ECategoria categoria)
        {
            var filter = Builders<Produto>.Filter.Eq(p => p.Categoria, categoria);
            return await _produtosCollection.Find(filter).ToListAsync();
        }

        public async Task<Produto> GetById(Guid idProduto)
        {
            var filter = Builders<Produto>.Filter.Eq(p => p.Id, idProduto);
            return await _produtosCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

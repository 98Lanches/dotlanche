using DotLanches.Domain.Entities;

namespace DotLanches.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        public Task Add(Produto produto);

        public Task<Produto> Edit(Produto produto);

        public Task<Produto> Delete(Guid idProduto);

        public Task<IEnumerable<Produto>> GetByCategoria(ECategoria categoria);

        public Task<Produto> GetById(Guid idProduto);
    }
}
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Gateways;
using DotLanches.Domain.Interfaces.Repositories;

namespace DotLanches.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoGateway(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Add(Produto produto) => await _produtoRepository.Add(produto);

        public async Task<Produto> Delete(Guid idProduto) => await _produtoRepository.Delete(idProduto);

        public async Task<Produto> Edit(Produto produto) => await _produtoRepository.Edit(produto);

        public async Task<IEnumerable<Produto>> GetByCategoria(ECategoria categoria) => await _produtoRepository.GetByCategoria(categoria);

        public async Task<Produto> GetById(Guid idProduto) => await _produtoRepository.GetById(idProduto);

        public async Task<Produto?> GetByName(string name) => await _produtoRepository.GetByName(name);
    }
}

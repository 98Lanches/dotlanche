using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Gateways;

namespace DotLanches.Application.UseCases
{
    public static class ProdutoUseCases
    {
        public static async Task RegisterNewProduto(Produto produto, IProdutoGateway gateway) => await gateway.Add(produto);
        
        public static async Task<Produto> EditExistingProduto(Produto produto, IProdutoGateway gateway) => await gateway.Edit(produto);

        public static async Task<Produto> RemoveProduto(Guid idProduto, IProdutoGateway gateway) => await gateway.Delete(idProduto);

        public static async Task<IEnumerable<Produto>> ShowAllProdutosForGivenCategory(ECategoria categoria, IProdutoGateway gateway) => await gateway.GetByCategoria(categoria);

        public static async Task<Produto> ShowSelectedProduto(Guid idProduto, IProdutoGateway gateway) => await gateway.GetById(idProduto);
    }
}
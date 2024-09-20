using DotLanches.Application.UseCases;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using DotLanches.Gateways;

namespace DotLanches.Controllers;

public class AdapterProdutoController
{
    private readonly IProdutoRepository _produtoRepository;

    public AdapterProdutoController(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<Guid> AddProduto(Produto produto)
    {
        var gateway = new ProdutoGateway(_produtoRepository);
        return await ProdutoUseCases.RegisterNewProduto(produto, gateway);
    }

    public async Task<Produto> EditProduto(Produto produto)
    {
        var gateway = new ProdutoGateway(_produtoRepository);
        var prod = await ProdutoUseCases.EditExistingProduto(produto, gateway);
        return prod;
    }

    public async Task<Produto> DeleteProduto(Guid idProduto)
    {
        var gateway = new ProdutoGateway(_produtoRepository);
        var prod = await ProdutoUseCases.RemoveProduto(idProduto, gateway);
        return prod;
    }

    public async Task<IEnumerable<Produto>> GetByCategoria(ECategoria categoria)
    {
        var gateway = new ProdutoGateway(_produtoRepository);
        var produtos = await ProdutoUseCases.ShowAllProdutosForGivenCategory(categoria, gateway);
        return produtos;
    }

    public async Task<Produto?> GetById(Guid idProduto)
    {
        var gateway = new ProdutoGateway(_produtoRepository);
        var produto = await ProdutoUseCases.ShowSelectedProduto(idProduto, gateway);
        return produto;
    }
}

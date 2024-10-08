﻿using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Gateways;
using DotLanches.UseCases.Exceptions;

namespace DotLanches.Application.UseCases
{
    public static class ProdutoUseCases
    {
        public static async Task<Guid> RegisterNewProduto(Produto produto, IProdutoGateway gateway)
        {
            var alreadyExists = await gateway.GetByName(produto.Name) is not null;
            if (alreadyExists)
                throw new ConflictException();

            await gateway.Add(produto);
            return produto.Id;
        }

        public static async Task<Produto> EditExistingProduto(Produto produto, IProdutoGateway gateway) => await gateway.Edit(produto);

        public static async Task<Produto> RemoveProduto(Guid idProduto, IProdutoGateway gateway) => await gateway.Delete(idProduto);

        public static async Task<IEnumerable<Produto>> ShowAllProdutosForGivenCategory(ECategoria categoria, IProdutoGateway gateway) => await gateway.GetByCategoria(categoria);

        public static async Task<Produto?> ShowSelectedProduto(Guid idProduto, IProdutoGateway gateway) => await gateway.GetById(idProduto);
    }
}
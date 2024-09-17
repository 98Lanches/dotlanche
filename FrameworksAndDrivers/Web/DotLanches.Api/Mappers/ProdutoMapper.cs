using DotLanches.Api.Dtos;
using DotLanches.Domain.Entities;

namespace DotLanches.Api.Mappers
{
    public static class ProdutoMapper
    {
        public static Produto ToDomainModel(this ProdutoDto produtoDto, Guid? id = null)
        {
            var domainModel = new Produto(id,
                                          produtoDto.Name,
                                          produtoDto.Description,
                                          produtoDto.Price,
                                          produtoDto.IdCategoria);

            return domainModel;
        }
    }
}
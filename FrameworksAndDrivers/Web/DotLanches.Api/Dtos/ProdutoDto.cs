using DotLanches.Domain.Entities;

namespace DotLanches.Api.Dtos
{
    public class ProdutoDto
    {
        public required string Name { get; set; }

        public required ECategoria Categoria { get; set; }

        public required string Description { get; set; }

        public required decimal Price { get; set; }
    }
}
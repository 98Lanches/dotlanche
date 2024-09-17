#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace DotLanches.Api.Dtos
{
    public class ProdutoDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ECategoria IdCategoria { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
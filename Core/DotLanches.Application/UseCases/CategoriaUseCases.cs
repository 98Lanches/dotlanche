using DotLanches.Domain.Entities;

namespace DotLanches.Application.UseCases
{
    public static class CategoriaUseCases
    {
        public static async Task<IDictionary<string,int>> ShowAllCategorias()
        {
            var categorias = Enum.GetValues(typeof(ECategoria))
                                 .Cast<ECategoria>();

            var categoriasDictionary = categorias.ToDictionary(
                categoria => categoria.ToString(),
                categoria => (int)categoria      
            );

            return await Task.FromResult(categoriasDictionary);
        }
    }
}

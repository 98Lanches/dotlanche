using DotLanches.Application.UseCases;
using DotLanches.Domain.Entities;

namespace DotLanches.Controllers;

public class AdapterCategoriaController
{
    public async Task<IDictionary<string,int>> GetAllCategorias()
    {
        return await CategoriaUseCases.ShowAllCategorias();
    }
}

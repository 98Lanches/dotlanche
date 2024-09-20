using DotLanches.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DotLanches.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    /// <summary>
    /// Busca todas as categorias.
    /// </summary>
    /// <returns>Lista de categorias.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(string[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var controller = new AdapterCategoriaController();
        var categorias = await controller.GetAllCategorias();
        return Ok(categorias);
    }
}

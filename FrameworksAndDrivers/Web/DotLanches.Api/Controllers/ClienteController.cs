using DotLanches.Api.Dtos;
using DotLanches.Api.Mappers;
using DotLanches.Controllers;
using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DotLanches.Api.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        
        /// <summary>
        /// Cria um novo cliente
        /// </summary>
        /// <param name="clienteDto">Dados do novo cliente</param>
        /// <returns>Cliente criado</returns> 
        [HttpPost]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ClienteDto clienteDto)
        {
            var adapterController = new AdapterClienteController(_clienteRepository);

            await adapterController.AddCliente(clienteDto.ToDomainModel());

            return StatusCode(StatusCodes.Status201Created);
        }
    
        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        /// <param name="clienteDto">Novos dados do cliente</param>
        /// <returns>Cliente atualizado</returns>
        [HttpPut("{idCliente}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] ClienteDto clienteDto)
        {
            var controller = new AdapterClienteController(_clienteRepository);
            var cliente = await controller.EditCliente(clienteDto.ToDomainModel());
            return Ok(cliente);
        }
        
        /// <summary>
        /// Remove um cliente
        /// </summary>
        /// <returns>Cliente removido</returns>
        [HttpDelete("{idCliente}")]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] string cpf)
        {
            var controller = new AdapterClienteController(_clienteRepository);
            var cliente = await controller.DeleteCliente(cpf);
            return Ok(cliente);
        }
        
        /// <summary>
        /// Busca todos os clientes
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var controller = new AdapterClienteController(_clienteRepository);
            var clienteList = await controller.GetAllClientes();
            return Ok(clienteList);
        }

        /// <summary>
        /// Busca de cliente por CPF
        /// </summary>
        /// <param name="cpf">cpf do cliente</param>
        /// <returns>Cliente</returns>
        [HttpGet("{cpf}")]
        [ProducesResponseType(typeof(IEnumerable<Cliente>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCpf(string cpf)
        {
            var adapterController = new AdapterClienteController(_clienteRepository);
            var cliente = await adapterController.GetByCpf(cpf); 
            return Ok(cliente);
        }
    }
}
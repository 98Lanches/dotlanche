using DotLanches.Domain.Entities;
using DotLanches.Domain.Exceptions;
using DotLanches.Domain.ValueObjects;
using DotLanches.Domain.Interfaces.Gateways;

namespace DotLanches.Application.UseCases
{
    public static class ClienteUseCases
    {
        public static async Task RegisterNewCliente(Cliente cliente, IClienteGateway gateway)
        {
            var clienteExists = await gateway.GetByCpf(cliente.Cpf.Number!);

            if (clienteExists is null)
                await gateway.Add(cliente);
            else
                throw new ClienteAlreadyExistsException(cliente.Cpf.Number!);
        }

        public static async Task<Cliente> EditExistingCliente(Cliente cliente, IClienteGateway gateway) => await gateway.Edit(cliente);

        public static async Task<Cliente> RemoveCliente(string cpf, IClienteGateway gateway) => await gateway.Delete(cpf);

        public static async Task<IEnumerable<Cliente>> ShowAllClientes(IClienteGateway gateway) => await gateway.GetAll();

        public static async Task<Cliente?> ShowClienteByTheirCpf(string cpf, IClienteGateway gateway)
        {
            var cpfNumber = new Cpf(cpf);

            var cliente = await gateway.GetByCpf(cpfNumber.Number);

            if (cliente is null)
                throw new ClienteNotFoundException(cpf);

            return cliente;
        }
    }
}

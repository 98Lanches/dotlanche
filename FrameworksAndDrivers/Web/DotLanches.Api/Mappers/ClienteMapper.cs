using DotLanches.Api.Dtos;
using DotLanches.Domain.Entities;

namespace DotLanches.Api.Mappers
{
    public static class ClienteMapper
    {
        public static Cliente ToDomainModel(this ClienteDto clienteDto)
        {
            var domainModel = new Cliente(clienteDto.Name, clienteDto.Cpf, clienteDto.Email);

            return domainModel;
        }
    }
}
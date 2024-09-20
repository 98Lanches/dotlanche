using DotLanches.Api.Dtos;
using DotLanches.Domain.Entities;

namespace DotLanches.Api.Mappers
{
    public static class ComboMapper
    {
        public static Combo ToDomainModel(this ComboDto comboDto)
        {
            var domainModel = new Combo(new Produto(comboDto.LancheId ?? Guid.Empty),
                                        new Produto(comboDto.AcompanhamentoId ?? Guid.Empty),
                                        new Produto(comboDto.BebidaId ?? Guid.Empty),
                                        new Produto(comboDto.SobremesaId ?? Guid.Empty));

            return domainModel;
        }
    }
}

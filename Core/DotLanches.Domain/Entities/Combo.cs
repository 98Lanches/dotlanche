#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Domain.Exceptions;

namespace DotLanches.Domain.Entities;

public class Combo
{
    public Guid Id { get; set; }
    public Produto? Lanche { get; set; }
    public Produto? Acompanhamento { get; set; }
    public Produto? Bebida { get; set; }
    public Produto? Sobremesa { get; set; }

    public Combo(Produto lanche,
                 Produto acompanhamento,
                 Produto bebida,
                 Produto sobremesa)
    {
        Id = Guid.NewGuid();
        Lanche = lanche;
        Acompanhamento = acompanhamento;
        Bebida = bebida;
        Sobremesa = sobremesa;

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (Lanche?.Id == Guid.Empty && Acompanhamento?.Id == Guid.Empty && Bebida?.Id == Guid.Empty && Sobremesa?.Id == Guid.Empty)
            throw new DomainValidationException(nameof(Produto));
    }

    public decimal CalculatePrice()
    {
        decimal price = 0;

        if (Lanche is not null)
            price += Lanche.Price;
        if (Acompanhamento is not null)
            price += Acompanhamento.Price;
        if (Bebida is not null)
            price += Bebida.Price;
        if (Sobremesa is not null)
            price += Sobremesa.Price;

        if (price <= 0)
            throw new DomainValidationException(nameof(price));

        return price; 
    }
}
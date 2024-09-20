#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
using DotLanches.Domain.Exceptions;

namespace DotLanches.Domain.Entities;

public class Produto
{
    public Guid Id { get; private set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ECategoria Categoria { get; set; }

    private Produto() { }

    public Produto(Guid id)
    {
        Id = id;
    }

    public Produto(Guid? id,
                   string name,
                   string description,
                   decimal price,
                   ECategoria categoria
                  )
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Categoria = categoria;

        ValidateEntity();
    }

    private void ValidateEntity()
    {
        if (string.IsNullOrEmpty(Name))
            throw new DomainValidationException(nameof(Name));
        if (string.IsNullOrEmpty(Description))
            throw new DomainValidationException(nameof(Description));
        if (Price <= 0)
            throw new DomainValidationException(nameof(Price));
        if (!Enum.IsDefined(typeof(ECategoria), Categoria))
            throw new DomainValidationException(nameof(Categoria));
    }
}
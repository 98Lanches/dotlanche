#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace DotLanches.Domain.Entities
{
    public enum EStatus
    {
        cancelado = 0,
        confirmado = 10,
        recebido = 20,
        emPreparo = 30,
        pronto = 40,
        finalizado = 50,
    }
}
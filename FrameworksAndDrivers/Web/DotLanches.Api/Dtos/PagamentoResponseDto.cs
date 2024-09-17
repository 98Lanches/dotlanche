namespace DotLanches.Api.Dtos;

public class PagamentoResponseDto
{
    public Guid IdPedido { get; set; }

    public bool IsAccepted { get; set; }
}

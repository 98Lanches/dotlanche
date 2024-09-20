namespace DotLanches.Domain.Entities;

public class Pagamento
{
    public Guid Id { get; private set; }

    public bool? IsAccepted { get; set; }
    
    public DateTime? RegisteredAt { get; set; }

    public Pagamento()
    {
        Id = Guid.NewGuid();
    }

    public void ConfirmPayment()
    {
        this.IsAccepted = true;
        this.RegisteredAt = DateTime.UtcNow;
    }

    public void RefusePayment()
    {
        this.IsAccepted = false;
        this.RegisteredAt = DateTime.UtcNow;
    }
}
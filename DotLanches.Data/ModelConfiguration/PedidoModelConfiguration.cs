using DotLanches.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotLanches.DataMongo.ModelConfiguration
{
    internal class PedidoModelConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ClienteCpf);
            builder.Property(p => p.CreatedAt);
            builder.Ignore(p => p.TotalPrice);
            builder.Property(p => p.QueueKey);
            builder.Property(p => p.AddedToQueueAt);
        }
    }
}

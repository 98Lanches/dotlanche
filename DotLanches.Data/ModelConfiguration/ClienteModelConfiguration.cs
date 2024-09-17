using DotLanches.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotLanches.DataMongo.ModelConfiguration
{
    internal class ClienteModelConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Cpf!.Number);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
        }
    }
}

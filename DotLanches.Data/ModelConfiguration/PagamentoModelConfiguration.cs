﻿using DotLanches.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotLanches.DataMongo.ModelConfiguration
{
    internal class PagamentoModelConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.IsAccepted);
            builder.Property(x => x.RegisteredAt);
        }
    }
}

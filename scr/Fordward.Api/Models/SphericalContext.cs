using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fordward.Api.Models
{
    public partial class SphericalContext : DbContext
    {
        public SphericalContext()
        {
        }

        public SphericalContext(DbContextOptions<SphericalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Factura> Facturas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Fordward");

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

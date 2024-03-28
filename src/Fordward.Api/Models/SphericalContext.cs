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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KENCHIC-PC\\SQLEXPRESS; Database=Spherical; User=Fordward; Password=Admin123*;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Fordward");

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Factura");

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>ESTADOS_FACTURA");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

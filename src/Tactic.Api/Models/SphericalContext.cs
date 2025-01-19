using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tactic.Api.Models
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

        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<CatalogoDetalle> CatalogoDetalles { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ListaPrecio> ListaPrecios { get; set; } = null!;
        public virtual DbSet<Parametro> Parametros { get; set; } = null!;
        public virtual DbSet<Sistema> Sistemas { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KENCHIC-PC\\SQLEXPRESS; Database=Spherical; User=Tactic; Password=Admin123*;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Tactic");

            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.ToTable("Catalogo");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.IdSistema)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idSistema");

                entity.HasOne(d => d.IdSistemaNavigation)
                    .WithMany(p => p.Catalogos)
                    .HasForeignKey(d => d.IdSistema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Catalogo_Sistema");
            });

            modelBuilder.Entity<CatalogoDetalle>(entity =>
            {
                entity.HasKey(e => new { e.IdCatalogo, e.Id });

                entity.ToTable("CatalogoDetalle");

                entity.Property(e => e.IdCatalogo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("idCatalogo");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Activo).HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ValorCadena)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ValorDecimal).HasColumnType("decimal(18, 5)");

                entity.HasOne(d => d.IdCatalogoNavigation)
                    .WithMany(p => p.CatalogoDetalles)
                    .HasForeignKey(d => d.IdCatalogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CatalogoDetalle_Catalogo");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.IdCiudad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("idCiudad");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre1)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre2)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ListaPrecio>(entity =>
            {
                entity.ToTable("ListaPrecio");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parametro>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK_ParametroSistema");

                entity.ToTable("Parametro");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.IdSistema)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idSistema");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSistemaNavigation)
                    .WithMany(p => p.Parametros)
                    .HasForeignKey(d => d.IdSistema)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Parametro_Sistema");
            });

            modelBuilder.Entity<Sistema>(entity =>
            {
                entity.ToTable("Sistema");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('P')")
                    .HasComment("CATALOGO=>ESTADOS_TICKET");

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.Prioridad)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

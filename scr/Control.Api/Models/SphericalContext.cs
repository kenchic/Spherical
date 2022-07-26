﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Control.Api.Models
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

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Proyecto> Proyectos { get; set; } = null!;
        public virtual DbSet<VProyecto> VProyectos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KENCHIC-PC\\SQLEXPRESS; Database=Spherical; User=Control; Password=Admin123*;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Control");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente", "Tactic");

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

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.ToTable("Proyecto");

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>CIUDAD");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Empresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>ESTADOS_PROYECTO");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FormaContacto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.IdentificacionResponsable)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.NombreResponsable)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Observacion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SistemaMedida)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelResponsable)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proyecto_Cliente");
            });

            modelBuilder.Entity<VProyecto>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vProyecto");

                entity.Property(e => e.IdCliente).HasColumnName("idCliente");

                entity.Property(e => e.Nombre1)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

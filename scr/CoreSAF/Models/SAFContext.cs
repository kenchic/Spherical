using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreSAF.Models
{
    public partial class SAFContext : DbContext
    {
        public SAFContext()
        {
        }

        public SAFContext(DbContextOptions<SAFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agente> Agentes { get; set; } = null!;
        public virtual DbSet<BdBodega> BdBodegas { get; set; } = null!;
        public virtual DbSet<BdCliente> BdClientes { get; set; } = null!;
        public virtual DbSet<BdContrato> BdContratos { get; set; } = null!;
        public virtual DbSet<BdCorte> BdCortes { get; set; } = null!;
        public virtual DbSet<BdCorteDetalle> BdCorteDetalles { get; set; } = null!;
        public virtual DbSet<BdCorteOrdenServicio> BdCorteOrdenServicios { get; set; } = null!;
        public virtual DbSet<BdCorteOrdenServicioDetalle> BdCorteOrdenServicioDetalles { get; set; } = null!;
        public virtual DbSet<BdDevolucion> BdDevolucions { get; set; } = null!;
        public virtual DbSet<BdDevolucionDetalle> BdDevolucionDetalles { get; set; } = null!;
        public virtual DbSet<BdDevolucionServicio> BdDevolucionServicios { get; set; } = null!;
        public virtual DbSet<BdDevolucionServicioDetalle> BdDevolucionServicioDetalles { get; set; } = null!;
        public virtual DbSet<BdDocumento> BdDocumentos { get; set; } = null!;
        public virtual DbSet<BdDocumentoDetalle> BdDocumentoDetalles { get; set; } = null!;
        public virtual DbSet<BdDocumentoTipo> BdDocumentoTipos { get; set; } = null!;
        public virtual DbSet<BdElemento> BdElementos { get; set; } = null!;
        public virtual DbSet<BdGrupoElemento> BdGrupoElementos { get; set; } = null!;
        public virtual DbSet<BdListaPrecio> BdListaPrecios { get; set; } = null!;
        public virtual DbSet<BdListaPrecioDetalle> BdListaPrecioDetalles { get; set; } = null!;
        public virtual DbSet<BdMantenimiento> BdMantenimientos { get; set; } = null!;
        public virtual DbSet<BdMantenimientoDetalle> BdMantenimientoDetalles { get; set; } = null!;
        public virtual DbSet<BdOrdenServicio> BdOrdenServicios { get; set; } = null!;
        public virtual DbSet<BdOrdenServicioDetalle> BdOrdenServicioDetalles { get; set; } = null!;
        public virtual DbSet<BdProveedor> BdProveedors { get; set; } = null!;
        public virtual DbSet<BdProyecto> BdProyectos { get; set; } = null!;
        public virtual DbSet<BdRemision> BdRemisions { get; set; } = null!;
        public virtual DbSet<BdRemisionDetalle> BdRemisionDetalles { get; set; } = null!;
        public virtual DbSet<BdReposicion> BdReposicions { get; set; } = null!;
        public virtual DbSet<BdReposicionDetalle> BdReposicionDetalles { get; set; } = null!;
        public virtual DbSet<BdReposicionServicio> BdReposicionServicios { get; set; } = null!;
        public virtual DbSet<BdReposicionServicioDetalle> BdReposicionServicioDetalles { get; set; } = null!;
        public virtual DbSet<BdUnidadMedidum> BdUnidadMedida { get; set; } = null!;
        public virtual DbSet<BdVentaDetalle> BdVentaDetalles { get; set; } = null!;
        public virtual DbSet<BdVentum> BdVenta { get; set; } = null!;
        public virtual DbSet<VBodega> VBodegas { get; set; } = null!;
        public virtual DbSet<VDocumento> VDocumentos { get; set; } = null!;
        public virtual DbSet<VDocumentoDetalle> VDocumentoDetalles { get; set; } = null!;
        public virtual DbSet<VElemento> VElementos { get; set; } = null!;
        public virtual DbSet<VListaPrecioDetalle> VListaPrecioDetalles { get; set; } = null!;
        public virtual DbSet<VProyectoPlantilla> VProyectoPlantillas { get; set; } = null!;
        public virtual DbSet<VRemision> VRemisions { get; set; } = null!;
        public virtual DbSet<VRemisionDetalle> VRemisionDetalles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KENCHIC-PC\\SQLEXPRESS; Database=SAF; User=SAF; Password=Admin123*;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SAF");

            modelBuilder.Entity<Agente>(entity =>
            {
                entity.ToTable("Agente");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdBodega>(entity =>
            {
                entity.ToTable("bdBodega");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.BdBodegas)
                    .HasForeignKey(d => d.IdProveedor)
                    .HasConstraintName("FK_bdBodegaProveedor");
            });

            modelBuilder.Entity<BdCliente>(entity =>
            {
                entity.ToTable("bdCliente");

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

                entity.Property(e => e.IdCiudad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("idCiudad");

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(103)
                    .IsUnicode(false)
                    .HasComputedColumnSql("(((((([Nombre1]+' ')+[Nombre2])+' ')+[Apellido1])+' ')+[Apellido2])", false);

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

            modelBuilder.Entity<BdContrato>(entity =>
            {
                entity.ToTable("bdContrato");

                entity.Property(e => e.IdAgente).HasColumnName("idAgente");

                entity.Property(e => e.IdListaPrecio).HasColumnName("idListaPrecio");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.InformacionBd).HasColumnName("InformacionBD");

                entity.HasOne(d => d.IdAgenteNavigation)
                    .WithMany(p => p.BdContratos)
                    .HasForeignKey(d => d.IdAgente)
                    .HasConstraintName("FK_bdContratoAgente");

                entity.HasOne(d => d.IdListaPrecioNavigation)
                    .WithMany(p => p.BdContratos)
                    .HasForeignKey(d => d.IdListaPrecio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdContratoListaPrecio");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.BdContratos)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdContratoProyecto");
            });

            modelBuilder.Entity<BdCorte>(entity =>
            {
                entity.ToTable("bdCorte");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo).HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdCorteIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdCorteBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdCorteIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdCorteBodegaOrigen");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.BdCortes)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdCorteProyecto");
            });

            modelBuilder.Entity<BdCorteDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdCorteDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdCorte).HasColumnName("idCorte");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");
            });

            modelBuilder.Entity<BdCorteOrdenServicio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdCorteOrdenServicio");

                entity.Property(e => e.BdCorte).HasColumnName("bdCorte");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo).HasColumnName("idDocumentoTipo");
            });

            modelBuilder.Entity<BdCorteOrdenServicioDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdCorteOrdenServicioDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdCorteOrdenServicio).HasColumnName("idCorteOrdenServicio");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");
            });

            modelBuilder.Entity<BdDevolucion>(entity =>
            {
                entity.ToTable("bdDevolucion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdConductor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idConductor");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.PesoEquipo).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ValorEquipo).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ValorTransporte).HasColumnType("numeric(8, 0)");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdDevolucionIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdDevolucionIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionBodegaOrigen");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.BdDevolucions)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionProyecto");
            });

            modelBuilder.Entity<BdDevolucionDetalle>(entity =>
            {
                entity.ToTable("bdDevolucionDetalle");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.HasOne(d => d.IdDevolucionNavigation)
                    .WithMany(p => p.BdDevolucionDetalles)
                    .HasForeignKey(d => d.IdDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionDetalleDevolucion");

                entity.HasOne(d => d.IdElementoNavigation)
                    .WithMany(p => p.BdDevolucionDetalles)
                    .HasForeignKey(d => d.IdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionDetalleElemento");
            });

            modelBuilder.Entity<BdDevolucionServicio>(entity =>
            {
                entity.ToTable("bdDevolucionServicio");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdDevolucionServicioIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionServicioBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdDevolucionServicioIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionServicioBodegaOrigen");

                entity.HasOne(d => d.IdDevolucionNavigation)
                    .WithMany(p => p.BdDevolucionServicios)
                    .HasForeignKey(d => d.IdDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionServicioDevolucion");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdDevolucionServicios)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionServicioDocumentoTipo");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.BdDevolucionServicios)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDevolucionServicioProveedor");
            });

            modelBuilder.Entity<BdDevolucionServicioDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdDevolucionServicioDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdDevolucionServicio).HasColumnName("idDevolucionServicio");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");
            });

            modelBuilder.Entity<BdDocumento>(entity =>
            {
                entity.ToTable("bdDocumento");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdDocumentoIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDocumentoBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdDocumentoIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDocumentoBodegaOrigen");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdDocumentos)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDocumento_bdDocumentoTipo");
            });

            modelBuilder.Entity<BdDocumentoDetalle>(entity =>
            {
                entity.ToTable("bdDocumentoDetalle");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.BdDocumentoDetalles)
                    .HasForeignKey(d => d.IdDocumento)
                    .HasConstraintName("FK_bdDocumento");

                entity.HasOne(d => d.IdElementoNavigation)
                    .WithMany(p => p.BdDocumentoDetalles)
                    .HasForeignKey(d => d.IdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdDocumentoDetalleElemento");
            });

            modelBuilder.Entity<BdDocumentoTipo>(entity =>
            {
                entity.ToTable("bdDocumentoTipo");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CantidadFilas).HasDefaultValueSql("((15))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Operacion)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdElemento>(entity =>
            {
                entity.ToTable("bdElemento");

                entity.Property(e => e.IdGrupoElemento).HasColumnName("idGrupoElemento");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdGrupoElementoNavigation)
                    .WithMany(p => p.BdElementos)
                    .HasForeignKey(d => d.IdGrupoElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdElementoGrupoElemento");

                entity.HasOne(d => d.IdUnidadMedidaNavigation)
                    .WithMany(p => p.BdElementos)
                    .HasForeignKey(d => d.IdUnidadMedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdElementoUnidadMedida");
            });

            modelBuilder.Entity<BdGrupoElemento>(entity =>
            {
                entity.ToTable("bdGrupoElemento");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdListaPrecio>(entity =>
            {
                entity.ToTable("bdListaPrecio");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdListaPrecioDetalle>(entity =>
            {
                entity.ToTable("bdListaPrecioDetalle");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdListaPrecio).HasColumnName("idListaPrecio");

                entity.HasOne(d => d.IdElementoNavigation)
                    .WithMany(p => p.BdListaPrecioDetalles)
                    .HasForeignKey(d => d.IdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdListaPrecioDetalleElemento");

                entity.HasOne(d => d.IdListaPrecioNavigation)
                    .WithMany(p => p.BdListaPrecioDetalles)
                    .HasForeignKey(d => d.IdListaPrecio)
                    .HasConstraintName("FK_bdListaPrecioDetalleListaPrecio");
            });

            modelBuilder.Entity<BdMantenimiento>(entity =>
            {
                entity.ToTable("bdMantenimiento");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdMantenimientoIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdMantenimientoBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdMantenimientoIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdMantenimientoBodegaOrigen");

                entity.HasOne(d => d.IdDevolucionNavigation)
                    .WithMany(p => p.BdMantenimientos)
                    .HasForeignKey(d => d.IdDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdMantenimientoDevolucion");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdMantenimientos)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdMantenimientoDocumentoTipo");
            });

            modelBuilder.Entity<BdMantenimientoDetalle>(entity =>
            {
                entity.ToTable("bdMantenimientoDetalle");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdMantenimiento).HasColumnName("idMantenimiento");

                entity.Property(e => e.TipoMantenimiento)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdMantenimientoNavigation)
                    .WithMany(p => p.BdMantenimientoDetalles)
                    .HasForeignKey(d => d.IdMantenimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdMantenimientoDetalleMantenimiento");
            });

            modelBuilder.Entity<BdOrdenServicio>(entity =>
            {
                entity.ToTable("bdOrdenServicio");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdOrdenServicioIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdOrdenServicioBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdOrdenServicioIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdOrdenServicioBodegaOrigen");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdOrdenServicios)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdOrdenServicioDocumentoTipo");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.BdOrdenServicios)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdOrdenServicioProveedor");

                entity.HasOne(d => d.IdRemisionNavigation)
                    .WithMany(p => p.BdOrdenServicios)
                    .HasForeignKey(d => d.IdRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdOrdenServicioRemision");
            });

            modelBuilder.Entity<BdOrdenServicioDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdOrdenServicioDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdOrdenServicio).HasColumnName("idOrdenServicio");
            });

            modelBuilder.Entity<BdProveedor>(entity =>
            {
                entity.ToTable("bdProveedor");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Iniciales)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdProyecto>(entity =>
            {
                entity.ToTable("bdProyecto");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado).HasDefaultValueSql("((1))");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.FormaContacto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCiudad)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("idCiudad")
                    .HasComment("Catalogo [CIUDADES]");

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
                    .WithMany(p => p.BdProyectos)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdProyectoCliente");
            });

            modelBuilder.Entity<BdRemision>(entity =>
            {
                entity.ToTable("bdRemision");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

                entity.Property(e => e.FechaPedido).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdConductor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idConductor")
                    .HasComment("CATALOGO [CONDUCTORES]");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.PesoEquipo).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ValorEquipo).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ValorTransporte).HasColumnType("numeric(8, 0)");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdRemisionIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemisionBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdRemisionIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemisionBodegaOrigen");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdRemisions)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemision_bdDocumentoTipo");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.BdRemisions)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemisionProyecto");
            });

            modelBuilder.Entity<BdRemisionDetalle>(entity =>
            {
                entity.ToTable("bdRemisionDetalle");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.HasOne(d => d.IdElementoNavigation)
                    .WithMany(p => p.BdRemisionDetalles)
                    .HasForeignKey(d => d.IdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemisionDetalleElemento");

                entity.HasOne(d => d.IdRemisionNavigation)
                    .WithMany(p => p.BdRemisionDetalles)
                    .HasForeignKey(d => d.IdRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdRemisionDetalleRemision");
            });

            modelBuilder.Entity<BdReposicion>(entity =>
            {
                entity.ToTable("bdReposicion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDevolucion).HasColumnName("idDevolucion");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdReposicionIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdReposicionIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionBodegaOrigen");

                entity.HasOne(d => d.IdDevolucionNavigation)
                    .WithMany(p => p.BdReposicions)
                    .HasForeignKey(d => d.IdDevolucion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionDevolucion");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdReposicions)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionDocumentoTipo");
            });

            modelBuilder.Entity<BdReposicionDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdReposicionDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdReposicion).HasColumnName("idReposicion");
            });

            modelBuilder.Entity<BdReposicionServicio>(entity =>
            {
                entity.ToTable("bdReposicionServicio");

                entity.Property(e => e.Estado)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDevolucionServicio).HasColumnName("idDevolucionServicio");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdReposicionServicioIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionServicioBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdReposicionServicioIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionServicioBodegaOrigen");

                entity.HasOne(d => d.IdDevolucionServicioNavigation)
                    .WithMany(p => p.BdReposicionServicios)
                    .HasForeignKey(d => d.IdDevolucionServicio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionServicioDevolucionServicio");

                entity.HasOne(d => d.IdDocumentoTipoNavigation)
                    .WithMany(p => p.BdReposicionServicios)
                    .HasForeignKey(d => d.IdDocumentoTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdReposicionServicioDocumentoTipo");
            });

            modelBuilder.Entity<BdReposicionServicioDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("bdReposicionServicioDetalle");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdReposicion).HasColumnName("idReposicion");
            });

            modelBuilder.Entity<BdUnidadMedidum>(entity =>
            {
                entity.ToTable("bdUnidadMedida");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BdVentaDetalle>(entity =>
            {
                entity.ToTable("bdVentaDetalle");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdVenta).HasColumnName("idVenta");

                entity.HasOne(d => d.IdElementoNavigation)
                    .WithMany(p => p.BdVentaDetalles)
                    .HasForeignKey(d => d.IdElemento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdVentaDetalleElemento");

                entity.HasOne(d => d.IdVentaNavigation)
                    .WithMany(p => p.BdVentaDetalles)
                    .HasForeignKey(d => d.IdVenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdVentaDetalleVenta");
            });

            modelBuilder.Entity<BdVentum>(entity =>
            {
                entity.ToTable("bdVenta");

                entity.Property(e => e.Estado)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.FechaSistema).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");

                entity.HasOne(d => d.IdBodegaDestinoNavigation)
                    .WithMany(p => p.BdVentumIdBodegaDestinoNavigations)
                    .HasForeignKey(d => d.IdBodegaDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdVentaBodegaDestino");

                entity.HasOne(d => d.IdBodegaOrigenNavigation)
                    .WithMany(p => p.BdVentumIdBodegaOrigenNavigations)
                    .HasForeignKey(d => d.IdBodegaOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdVentaBodegaOrigen");

                entity.HasOne(d => d.IdRemisionNavigation)
                    .WithMany(p => p.BdVenta)
                    .HasForeignKey(d => d.IdRemision)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bdVentaRemision");
            });

            modelBuilder.Entity<VBodega>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBodega");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ProveedorNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProyectoNombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VDocumento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDocumento");

                entity.Property(e => e.BodegaDestinoNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BodegaOrigenNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentoTipoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");
            });

            modelBuilder.Entity<VDocumentoDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDocumentoDetalle");

                entity.Property(e => e.BodegaDestinoNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BodegaOrigenNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ElementoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");
            });

            modelBuilder.Entity<VElemento>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vElemento");

                entity.Property(e => e.GrupoElementoNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdGrupoElemento).HasColumnName("idGrupoElemento");

                entity.Property(e => e.IdUnidadMedida).HasColumnName("idUnidadMedida");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Referencia)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UnidadMedidaNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VListaPrecioDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vListaPrecioDetalle");

                entity.Property(e => e.ElementoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdListaPrecio).HasColumnName("idListaPrecio");

                entity.Property(e => e.ListaPrecioNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VProyectoPlantilla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vProyectoPlantilla");

                entity.Property(e => e.Cantidad)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Documento)
                    .HasMaxLength(85)
                    .IsUnicode(false);

                entity.Property(e => e.Elemento)
                    .HasMaxLength(103)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VRemision>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vRemision");

                entity.Property(e => e.BodegaDestinoNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.BodegaOrigenNombre)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentoTipoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

                entity.Property(e => e.FechaPedido).HasColumnType("datetime");

                entity.Property(e => e.IdBodegaDestino).HasColumnName("idBodegaDestino");

                entity.Property(e => e.IdBodegaOrigen).HasColumnName("idBodegaOrigen");

                entity.Property(e => e.IdConductor)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idConductor");

                entity.Property(e => e.IdDocumentoTipo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDocumentoTipo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.PesoEquipo).HasColumnType("numeric(6, 2)");

                entity.Property(e => e.ValorEquipo).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ValorTransporte).HasColumnType("numeric(8, 0)");
            });

            modelBuilder.Entity<VRemisionDetalle>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vRemisionDetalle");

                entity.Property(e => e.ElementoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdElemento).HasColumnName("idElemento");

                entity.Property(e => e.IdRemision).HasColumnName("idRemision");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

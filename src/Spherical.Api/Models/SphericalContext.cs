using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Spherical.Api.Models;

public partial class SphericalContext : DbContext
{
    public SphericalContext()
    {
    }

    public SphericalContext(DbContextOptions<SphericalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<Catalogo> Catalogos { get; set; }

    public virtual DbSet<CatalogoDetalle> CatalogoDetalles { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<DocumentoDetalle> DocumentoDetalles { get; set; }

    public virtual DbSet<DocumentoTipo> DocumentoTipos { get; set; }

    public virtual DbSet<Elemento> Elementos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<ListaPrecio> ListaPrecios { get; set; }

    public virtual DbSet<ListaPrecioDetalle> ListaPrecioDetalles { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Parametro> Parametros { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PermisoOpcion> PermisoOpcions { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Sistema> Sistemas { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VusuarioPermiso> VusuarioPermisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=Spherical; User=spherical; Password=Admin123*;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Spherical");

        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Bodegas");

            entity.ToTable("Bodega");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

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
                .IsUnicode(false);
            entity.Property(e => e.IdSistema)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("idSistema");

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Catalogos)
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
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Activo).HasDefaultValue(true, "DF_CatalogoDetalle_Activo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ValorCadena)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ValorDecimal).HasColumnType("decimal(18, 5)");

            entity.HasOne(d => d.IdCatalogoNavigation).WithMany(p => p.CatalogoDetalles)
                .HasForeignKey(d => d.IdCatalogo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CatalogoDetalle_Catalogo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Clientes");

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
                .IsUnicode(false);
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

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.ToTable("Documento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
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

        modelBuilder.Entity<DocumentoDetalle>(entity =>
        {
            entity.ToTable("DocumentoDetalle");

            entity.Property(e => e.IdDocumento).HasColumnName("idDocumento");
            entity.Property(e => e.IdElemento).HasColumnName("idElemento");
        });

        modelBuilder.Entity<DocumentoTipo>(entity =>
        {
            entity.ToTable("DocumentoTipo");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Operacion)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Elemento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Elementos");

            entity.ToTable("Elemento", tb => tb.HasTrigger("trg_elemento_crear_listaprecios"));

            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdGrupoElemento)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.IdUnidadMedida)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Referencia)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.ToTable("Factura");

            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.ToTable("Grupo");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasMany(d => d.IdRols).WithMany(p => p.IdGrupos)
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GrupoRol_Rol"),
                    l => l.HasOne<Grupo>().WithMany()
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GrupoRol_Grupo"),
                    j =>
                    {
                        j.HasKey("IdGrupo", "IdRol");
                        j.ToTable("GrupoRol");
                        j.IndexerProperty<string>("IdGrupo")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idGrupo");
                        j.IndexerProperty<string>("IdRol")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idRol");
                    });

            entity.HasMany(d => d.IdUsuarios).WithMany(p => p.IdGrupos)
                .UsingEntity<Dictionary<string, object>>(
                    "GrupoUsuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GrupoUsuario_Usuario"),
                    l => l.HasOne<Grupo>().WithMany()
                        .HasForeignKey("IdGrupo")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_GrupoUsuario_Grupo"),
                    j =>
                    {
                        j.HasKey("IdGrupo", "IdUsuario");
                        j.ToTable("GrupoUsuario");
                        j.IndexerProperty<string>("IdGrupo")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idGrupo");
                        j.IndexerProperty<string>("IdUsuario")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idUsuario");
                    });
        });

        modelBuilder.Entity<ListaPrecio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ListasPrecios");

            entity.ToTable("ListaPrecio");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ListaPrecioDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdListaPrecio, e.IdElemento }).HasName("ListaPrecioDetalle_PK");

            entity.ToTable("ListaPrecioDetalle");

            entity.HasOne(d => d.IdElementoNavigation).WithMany(p => p.ListaPrecioDetalles)
                .HasForeignKey(d => d.IdElemento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ListaPrecioDetalle_Elemento_FK");

            entity.HasOne(d => d.IdListaPrecioNavigation).WithMany(p => p.ListaPrecioDetalles)
                .HasForeignKey(d => d.IdListaPrecio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ListaPrecioDetalle_ListaPrecio_FK");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Icono)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IdMenu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idMenu");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.InverseIdMenuNavigation)
                .HasForeignKey(d => d.IdMenu)
                .HasConstraintName("FK_MenuPadre");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.ToTable("Opcion");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Parametro>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_ParametroSistema");

            entity.ToTable("Parametro");

            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
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

            entity.HasOne(d => d.IdSistemaNavigation).WithMany(p => p.Parametros)
                .HasForeignKey(d => d.IdSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parametro_Sistema");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.ToTable("Permiso");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdMenus).WithMany(p => p.IdPermisos)
                .UsingEntity<Dictionary<string, object>>(
                    "PermisoMenu",
                    r => r.HasOne<Menu>().WithMany()
                        .HasForeignKey("IdMenu")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PermisoMenu_Menu"),
                    l => l.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PermisoMenu_Permiso"),
                    j =>
                    {
                        j.HasKey("IdPermiso", "IdMenu");
                        j.ToTable("PermisoMenu");
                        j.IndexerProperty<string>("IdPermiso")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idPermiso");
                        j.IndexerProperty<string>("IdMenu")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idMenu");
                    });
        });

        modelBuilder.Entity<PermisoOpcion>(entity =>
        {
            entity.HasKey(e => new { e.IdPermiso, e.IdOpcion });

            entity.ToTable("PermisoOpcion");

            entity.Property(e => e.IdPermiso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idPermiso");
            entity.Property(e => e.IdOpcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("idOpcion");

            entity.HasOne(d => d.IdOpcionNavigation).WithMany(p => p.PermisoOpcions)
                .HasForeignKey(d => d.IdOpcion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermisoOpcion_Opcion");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.PermisoOpcions)
                .HasForeignKey(d => d.IdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PermisoOpcion_Permiso");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Proyectos");

            entity.ToTable("Proyecto");

            entity.Property(e => e.Ciudad)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false);
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

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Proyecto_Cliente");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.ToTable("Rol");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.IdPermisos).WithMany(p => p.IdRols)
                .UsingEntity<Dictionary<string, object>>(
                    "RolPermiso",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("IdPermiso")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolPermiso_Permiso"),
                    l => l.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolPermiso_Rol"),
                    j =>
                    {
                        j.HasKey("IdRol", "IdPermiso");
                        j.ToTable("RolPermiso");
                        j.IndexerProperty<string>("IdRol")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idRol");
                        j.IndexerProperty<string>("IdPermiso")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idPermiso");
                    });
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
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("P", "DF_Ticket_Estado");
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

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Empresa)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Identificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Usuario");

            entity.HasMany(d => d.IdRols).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UsuarioRol_Rol"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UsuarioRol_Usuario"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol");
                        j.ToTable("UsuarioRol");
                        j.IndexerProperty<string>("IdUsuario")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idUsuario");
                        j.IndexerProperty<string>("IdRol")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("idRol");
                    });
        });

        modelBuilder.Entity<VusuarioPermiso>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUsuarioPermiso");

            entity.Property(e => e.Opcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

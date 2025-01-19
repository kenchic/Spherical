using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Defender.Api.Models
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

        public virtual DbSet<Grupo> Grupos { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Opcion> Opcions { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<PermisoOpcion> PermisoOpcions { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=KENCHIC-PC\\SQLEXPRESS; Database=Spherical; User=Defender; Password=Admin123*;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Defender");

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

                entity.HasMany(d => d.IdRols)
                    .WithMany(p => p.IdGrupos)
                    .UsingEntity<Dictionary<string, object>>(
                        "GrupoRol",
                        l => l.HasOne<Rol>().WithMany().HasForeignKey("IdRol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GrupoRol_Rol"),
                        r => r.HasOne<Grupo>().WithMany().HasForeignKey("IdGrupo").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GrupoRol_Grupo"),
                        j =>
                        {
                            j.HasKey("IdGrupo", "IdRol");

                            j.ToTable("GrupoRol");

                            j.IndexerProperty<string>("IdGrupo").HasMaxLength(50).IsUnicode(false).HasColumnName("idGrupo");

                            j.IndexerProperty<string>("IdRol").HasMaxLength(50).IsUnicode(false).HasColumnName("idRol");
                        });

                entity.HasMany(d => d.IdUsuarios)
                    .WithMany(p => p.IdGrupos)
                    .UsingEntity<Dictionary<string, object>>(
                        "GrupoUsuario",
                        l => l.HasOne<Usuario>().WithMany().HasForeignKey("IdUsuario").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GrupoUsuario_Usuario"),
                        r => r.HasOne<Grupo>().WithMany().HasForeignKey("IdGrupo").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_GrupoUsuario_Grupo"),
                        j =>
                        {
                            j.HasKey("IdGrupo", "IdUsuario");

                            j.ToTable("GrupoUsuario");

                            j.IndexerProperty<string>("IdGrupo").HasMaxLength(50).IsUnicode(false).HasColumnName("idGrupo");

                            j.IndexerProperty<string>("IdUsuario").HasMaxLength(50).IsUnicode(false).HasColumnName("idUsuario");
                        });
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

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.InverseIdMenuNavigation)
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

                entity.HasMany(d => d.IdMenus)
                    .WithMany(p => p.IdPermisos)
                    .UsingEntity<Dictionary<string, object>>(
                        "PermisoMenu",
                        l => l.HasOne<Menu>().WithMany().HasForeignKey("IdMenu").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PermisoMenu_Menu"),
                        r => r.HasOne<Permiso>().WithMany().HasForeignKey("IdPermiso").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_PermisoMenu_Permiso"),
                        j =>
                        {
                            j.HasKey("IdPermiso", "IdMenu");

                            j.ToTable("PermisoMenu");

                            j.IndexerProperty<string>("IdPermiso").HasMaxLength(50).IsUnicode(false).HasColumnName("idPermiso");

                            j.IndexerProperty<string>("IdMenu").HasMaxLength(50).IsUnicode(false).HasColumnName("idMenu");
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

                entity.HasOne(d => d.IdOpcionNavigation)
                    .WithMany(p => p.PermisoOpcions)
                    .HasForeignKey(d => d.IdOpcion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermisoOpcion_Opcion");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.PermisoOpcions)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermisoOpcion_Permiso");
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

                entity.HasMany(d => d.IdPermisos)
                    .WithMany(p => p.IdRols)
                    .UsingEntity<Dictionary<string, object>>(
                        "RolPermiso",
                        l => l.HasOne<Permiso>().WithMany().HasForeignKey("IdPermiso").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_RolPermiso_Permiso"),
                        r => r.HasOne<Rol>().WithMany().HasForeignKey("IdRol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_RolPermiso_Rol"),
                        j =>
                        {
                            j.HasKey("IdRol", "IdPermiso");

                            j.ToTable("RolPermiso");

                            j.IndexerProperty<string>("IdRol").HasMaxLength(50).IsUnicode(false).HasColumnName("idRol");

                            j.IndexerProperty<string>("IdPermiso").HasMaxLength(50).IsUnicode(false).HasColumnName("idPermiso");
                        });
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
                    .IsUnicode(false)
                    .HasComment("CATALOGO=>EMPRESA");

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

                entity.HasMany(d => d.IdRols)
                    .WithMany(p => p.IdUsuarios)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsuarioRol",
                        l => l.HasOne<Rol>().WithMany().HasForeignKey("IdRol").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsuarioRol_Rol"),
                        r => r.HasOne<Usuario>().WithMany().HasForeignKey("IdUsuario").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_UsuarioRol_Usuario"),
                        j =>
                        {
                            j.HasKey("IdUsuario", "IdRol");

                            j.ToTable("UsuarioRol");

                            j.IndexerProperty<string>("IdUsuario").HasMaxLength(50).IsUnicode(false).HasColumnName("idUsuario");

                            j.IndexerProperty<string>("IdRol").HasMaxLength(50).IsUnicode(false).HasColumnName("idRol");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

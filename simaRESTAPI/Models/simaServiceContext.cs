using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace simaRESTAPI.Models
{
    public partial class simaServiceContext : DbContext
    {
        public simaServiceContext()
        {
        }

        public simaServiceContext(DbContextOptions<simaServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<Remisiones> Remisiones { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-F3VTPFL;Database=simaService;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Imagenes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Estilo)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).IsRequired();
            });

            modelBuilder.Entity<Remisiones>(entity =>
            {
                entity.HasKey(e => e.Remision);

                entity.Property(e => e.Remision)
                    .HasColumnName("remision")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Orden)
                    .HasColumnName("orden")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Pedido)
                    .HasColumnName("pedido")
                    .HasComputedColumnSql("(CONVERT([int],substring([remision],(2),(6)),(0)))");

                entity.Property(e => e.Preorden)
                    .HasColumnName("preorden")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tienda)
                    .HasColumnName("tienda")
                    .HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.CodUsuario);

                entity.Property(e => e.CodUsuario)
                    .HasColumnName("Cod_usuario")
                    .ValueGeneratedNever();

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TiendaBase)
                    .IsRequired()
                    .HasColumnName("Tienda_base")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

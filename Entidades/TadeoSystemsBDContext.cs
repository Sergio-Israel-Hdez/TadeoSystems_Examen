using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entidades
{
    public partial class TadeoSystemsBDContext : DbContext
    {
        public TadeoSystemsBDContext()
        {
        }

        public TadeoSystemsBDContext(DbContextOptions<TadeoSystemsBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadoHabilidad> EmpleadoHabilidad { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=TadeoSystemsBD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.IdArea)
                    .HasName("PK__Area__2FC141AADA25DBD1");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9EF11D6CA1");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngreso)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Foto).IsRequired();

                entity.Property(e => e.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.IdArea)
                    .HasConstraintName("FK__Empleado__IdArea__145C0A3F");

                entity.HasOne(d => d.IdJefeNavigation)
                    .WithMany(p => p.InverseIdJefeNavigation)
                    .HasForeignKey(d => d.IdJefe)
                    .HasConstraintName("FK__Empleado__IdJefe__1367E606");
            });

            modelBuilder.Entity<EmpleadoHabilidad>(entity =>
            {
                entity.HasKey(e => e.IdHabilidad)
                    .HasName("PK__Empleado__A6B5610A0AACCAD4");

                entity.ToTable("Empleado_Habilidad");

                entity.Property(e => e.NombreHabilidad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.EmpleadoHabilidad)
                    .HasForeignKey(d => d.IdEmpleado)
                    .HasConstraintName("FK__Empleado___IdEmp__173876EA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

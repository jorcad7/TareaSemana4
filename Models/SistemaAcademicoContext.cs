using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistema.Models;

public partial class SistemaAcademicoContext : DbContext
{
    public SistemaAcademicoContext()
    {
    }

    public SistemaAcademicoContext(DbContextOptions<SistemaAcademicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Certificado> Certificados { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Inscripcion> Inscripcions { get; set; }

  // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySQL("server=localhost; port=3306; database=sistema_academico; uid=root; password=root;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Activo).HasDefaultValueSql("'1'");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Certificado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("certificado");

            entity.HasIndex(e => e.CodigoUnico, "CodigoUnico").IsUnique();

            entity.HasIndex(e => e.InscripcionId, "InscripcionId").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CalificacionFinal).HasPrecision(5);
            entity.Property(e => e.CodigoUnico).HasMaxLength(100);
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.InscripcionId).HasColumnType("int(11)");

            entity.HasOne(d => d.Inscripcion).WithOne(p => p.Certificado)
                .HasForeignKey<Certificado>(d => d.InscripcionId)
                .HasConstraintName("fk_certificado_inscripcion");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("curso");

            entity.HasIndex(e => e.CategoriaId, "fk_curso_categoria");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CategoriaId).HasColumnType("int(11)");
            entity.Property(e => e.Creditos).HasColumnType("int(11)");
            entity.Property(e => e.Precio).HasPrecision(10);
            entity.Property(e => e.Titulo).HasMaxLength(150);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_curso_categoria");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Inscripcion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inscripcion");

            entity.HasIndex(e => e.CursoId, "fk_inscripcion_curso");

            entity.HasIndex(e => e.EstudianteId, "fk_inscripcion_estudiante");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.CursoId).HasColumnType("int(11)");
            entity.Property(e => e.Estado).HasMaxLength(50);
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");

            entity.HasOne(d => d.Curso).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.CursoId)
                .HasConstraintName("fk_inscripcion_curso");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Inscripcions)
                .HasForeignKey(d => d.EstudianteId)
                .HasConstraintName("fk_inscripcion_estudiante");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

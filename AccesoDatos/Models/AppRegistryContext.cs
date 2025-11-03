using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Models;

public partial class AppRegistryContext : DbContext
{
    public AppRegistryContext()
    {
    }

    public AppRegistryContext(DbContextOptions<AppRegistryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<ArchType> ArchTypes { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<BusinessOwner> BusinessOwners { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<LifeCycleStage> LifeCycleStages { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WATSQLDEV15; Encrypt=False; DataBase=AppRegistry;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__alumno__3213E83F1C5759B6");

            entity.ToTable("alumno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.AppId).HasName("PK__Applicat__6F8A0A341F43099E");

            entity.ToTable(tb => tb.HasTrigger("trg_Applications_Update"));

            entity.Property(e => e.AppId).HasColumnName("app_id");
            entity.Property(e => e.Businesscritically)
                .HasMaxLength(50)
                .HasColumnName("businesscritically");
            entity.Property(e => e.Certified).HasColumnName("certified");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.LastChangeDate)
                .HasPrecision(0)
                .HasColumnName("last_change_date");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Platform)
                .HasMaxLength(50)
                .HasColumnName("platform");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Technologystack)
                .HasMaxLength(50)
                .HasColumnName("technologystack");
            entity.Property(e => e.Userbase)
                .HasMaxLength(50)
                .HasColumnName("userbase");
        });

        modelBuilder.Entity<ApplicationType>(entity =>
        {
            entity.HasKey(e => e.Idapptype).HasName("PK__Applicat__8DA9FCC20D4EA60F");

            entity.ToTable("ApplicationType");

            entity.Property(e => e.ApptypeName)
                .HasMaxLength(50)
                .HasColumnName("Apptype_name");
        });

        modelBuilder.Entity<ArchType>(entity =>
        {
            entity.HasKey(e => e.Idarchtype).HasName("PK__ArchType__26801B371A15557A");

            entity.ToTable("ArchType");

            entity.Property(e => e.ArchtypeName)
                .HasMaxLength(50)
                .HasColumnName("Archtype_name");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__asignatu__3213E83F219B0FA3");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Creditos).HasColumnName("creditos");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Profesor)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profesor");

            entity.HasOne(d => d.ProfesorNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.Profesor)
                .HasConstraintName("FK__asignatur__profe__52593CB8");
        });

        modelBuilder.Entity<BusinessOwner>(entity =>
        {
            entity.HasKey(e => e.IdbusinessOwner).HasName("PK__Business__C79CF52FA24EA9E5");

            entity.ToTable("BusinessOwner");

            entity.Property(e => e.BusinessOwnerName)
                .HasMaxLength(50)
                .HasColumnName("BusinessOwner_name");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__califica__3213E83FD24BFE4F");

            entity.ToTable("calificacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.MatriculaId).HasColumnName("matriculaId");
            entity.Property(e => e.Nota).HasColumnName("nota");
            entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");

            entity.HasOne(d => d.Matricula).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.MatriculaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__calificac__matri__59063A47");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Iddepartment).HasName("PK__Departme__FE2F9795C38A1FE8");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .HasColumnName("Department_name");
        });

        modelBuilder.Entity<LifeCycleStage>(entity =>
        {
            entity.HasKey(e => e.Idlifecyclestage).HasName("PK__LifeCylc__CEF767D090D82E1F");

            entity.ToTable("LifeCycleStage");

            entity.Property(e => e.LifeCycleStageName)
                .HasMaxLength(50)
                .HasColumnName("LifeCycleStage_name");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__matricul__3213E83FC89B1E28");

            entity.ToTable("matricula");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlumnoId).HasColumnName("alumnoId");
            entity.Property(e => e.AsignaturaId).HasColumnName("asignaturaId");

            entity.HasOne(d => d.Alumno).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matricula__alumn__5535A963");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__matricula__asign__5629CD9C");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK__profesor__9AFF8FC70E9E48BD");

            entity.ToTable("profesor");

            entity.Property(e => e.Usuario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usuario");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pass");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

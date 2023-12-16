using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCStudentsApp.PeopleModels;

public partial class SchoolDatabaseContext : DbContext
{
    public SchoolDatabaseContext()
    {
    }

    public SchoolDatabaseContext(DbContextOptions<SchoolDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassDetail> ClassDetails { get; set; }

    public virtual DbSet<CurricularUnit> CurricularUnits { get; set; }

    public virtual DbSet<Objective> Objectives { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=sql_server2022;Database=SchoolDatabase;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True;MultipleActiveResultSets=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC0774B308DC");

            entity.HasIndex(e => new { e.ClassDetailsId, e.StudentId }, "UQ__Classes__AEB8596FA46AE9F2").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.ClassDetails).WithMany(p => p.Classes)
                .HasForeignKey(d => d.ClassDetailsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classes__ClassDe__44FF419A");

            entity.HasOne(d => d.Student).WithMany(p => p.Classes)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classes__Student__45F365D3");
        });

        modelBuilder.Entity<ClassDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class_De__3214EC076B6457B8");

            entity.ToTable("Class_Details");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameClass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Name_class");
            entity.Property(e => e.YearClass).HasColumnName("Year_class");

            entity.HasOne(d => d.Teacher).WithMany(p => p.ClassDetails)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class_Det__Teach__412EB0B6");
        });

        modelBuilder.Entity<CurricularUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Curricul__3214EC07FE5D50E4");

            entity.ToTable("Curricular_Units");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NameUnits)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Name_units");
        });

        modelBuilder.Entity<Objective>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Objectiv__3214EC07618E56F7");

            entity.ToTable("Objective");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FkCurricularUnits).HasColumnName("fk_Curricular_Units");

            entity.HasOne(d => d.FkCurricularUnitsNavigation).WithMany(p => p.Objectives)
                .HasForeignKey(d => d.FkCurricularUnits)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Objective__fk_Cu__3E52440B");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__People__3214EC0766D48673");

            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.FkRole).HasColumnName("fkRole");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Last_Name");

            entity.HasOne(d => d.FkRoleNavigation).WithMany(p => p.People)
                .HasForeignKey(d => d.FkRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__People__fkRole__398D8EEE");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07BB4DDCD8");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DescriptionRole)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Description_role");
            entity.Property(e => e.LabelRole)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Label_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

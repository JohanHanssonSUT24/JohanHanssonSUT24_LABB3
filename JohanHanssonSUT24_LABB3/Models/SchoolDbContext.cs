using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JohanHanssonSUT24_LABB3.Models;

public partial class SchoolDbContext : DbContext
{
    public SchoolDbContext()
    {
    }

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Database=SchoolDB;Integrated Security=True;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3214EC0702810ED1");

            entity.ToTable("Class");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeacherStaffId).HasColumnName("Teacher_StaffId");

            entity.HasOne(d => d.TeacherStaff).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TeacherStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Class__Teacher_S__398D8EEE");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.GradeId).HasName("PK__Grades__54F87A577ACA404E");

            entity.Property(e => e.GradeChar)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Student).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Grades__StudentI__412EB0B6");

            entity.HasOne(d => d.Subject).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__Grades__SubjectI__4316F928");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Grades)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Grades__TeacherI__4222D4EF");
        });

        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.OccupationId).HasName("PK__Occupati__891711AD21AC5EE4");

            entity.ToTable("Occupation");

            entity.Property(e => e.OccupationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AB178C2F60A1");

            entity.Property(e => e.StaffName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Occupation).WithMany(p => p.Staff)
                .HasForeignKey(d => d.OccupationId)
                .HasConstraintName("FK__Staff__Occupatio__45F365D3");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudId).HasName("PK__Students__F5C0A7FFB4E01423");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Students__ClassI__3C69FB99");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA3A801A63CA7");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

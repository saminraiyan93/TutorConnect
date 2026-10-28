using System;
using System.Collections.Generic;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public partial class TutorConnectContext : DbContext
{
    public TutorConnectContext()
    {
    }

    public TutorConnectContext(DbContextOptions<TutorConnectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<StudentRequest> StudentRequests { get; set; }

    public virtual DbSet<TutorOffering> TutorOfferings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DBConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Courses_Departments");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StudentRequest>(entity =>
        {
            entity.Property(e => e.Availability)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BudgetType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConnectValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConnectVia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CoverageType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.PostStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PostTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeachingMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TopicDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentRequests)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentRequests_Courses");

            entity.HasOne(d => d.User).WithMany(p => p.StudentRequests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentRequests_Users");
        });

        modelBuilder.Entity<TutorOffering>(entity =>
        {
            entity.Property(e => e.Availability)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactVia)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CoverageType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.PostStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PostTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PricingType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TeachingMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TopicDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Course).WithMany(p => p.TutorOfferings)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TutorOfferings_Courses");

            entity.HasOne(d => d.User).WithMany(p => p.TutorOfferings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TutorOfferings_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.AccountStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

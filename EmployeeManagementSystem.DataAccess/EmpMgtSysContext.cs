using EmployeeManagementSystem.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.DataAccess
{
    public class EmpMgtSysContext : DbContext
    {
        public EmpMgtSysContext(DbContextOptions<EmpMgtSysContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                // Table name
                entity.ToTable("Employee");

                // Primary Key
                entity.HasKey(e => e.EmployeeID);

                // Properties
                entity.Property(e => e.EmployeeID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15);

                entity.Property(e => e.DepartmentID)
                    .IsRequired();

                entity.Property(e => e.Position)
                    .HasMaxLength(100);

                entity.Property(e => e.JoiningDate)
                    .IsRequired();

                entity.Property(e => e.Deleted)
                    .HasDefaultValue(false);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValue(true);

                // Constraints
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                // Relationships
                entity.HasOne(e => e.Department)
                    .WithMany(d => d.Employee)
                    .HasForeignKey(e => e.DepartmentID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Employee_Department");
            });
            modelBuilder.Entity<PerformanceReview>(entity =>
            {
                // Table name
                entity.ToTable("PerformanceReview");

                // Primary Key
                entity.HasKey(e => e.ReviewID);

                // Properties
                entity.Property(e => e.ReviewID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EmployeeID)
                    .IsRequired();

                entity.Property(e => e.ReviewDate)
                    .IsRequired();

                entity.Property(e => e.ReviewScore)
                    .HasColumnType("TINYINT")
                    .IsRequired(false);

                entity.HasCheckConstraint("CK_ReviewScore", "[ReviewScore] >= 1 AND [ReviewScore] <= 10");

                entity.Property(e => e.ReviewNotes)
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedDate)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(e => e.Employee)
                    .WithMany(emp => emp.PerformanceReview)
                    .HasForeignKey(e => e.EmployeeID)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PerformanceReview_Employee");
            });
            modelBuilder.Entity<Department>(entity =>
            {
                // Table name
                entity.ToTable("Department");

                // Primary Key
                entity.HasKey(e => e.DepartmentID);

                // Properties
                entity.Property(e => e.DepartmentID)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ManagerID)
                    .IsRequired(false);

                entity.Property(e => e.Budget)
                    .HasColumnType("DECIMAL(18,2)");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedDate)
                    .HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasOne(e => e.Manager)
                    .WithOne()
                    .HasForeignKey<Department>(e => e.ManagerID)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Department_Employee");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}

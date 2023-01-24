using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationMicroService.Models;

public partial class AuthorizationContext : DbContext
{
    public AuthorizationContext()
    {
    }

    public AuthorizationContext(DbContextOptions<AuthorizationContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<LoginData> LoginData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
          IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
          var connectionString = configuration["ConnectionString"];
          optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FirstName)
                .HasMaxLength(48)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(48)
                .IsFixedLength();

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeTypeId)
                .HasConstraintName("FK__Employees__Emplo__7F2BE32F");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LoginData>(entity =>
        {
            entity.HasKey(e => e.LoginId);

            entity.Property(e => e.Login)
                .HasMaxLength(128)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsFixedLength();
            entity.Property(e => e.RefreshToken).HasMaxLength(100);
            entity.Property(e => e.RefreshTokenExpireDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.LoginData)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginData_Employees");
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.WorkEndDate).HasColumnType("date");
            entity.Property(e => e.WorkStartDate).HasColumnType("date");
        });

        modelBuilder.Entity<LoginData>(entity =>
        {
            entity.HasKey(e => e.LoginId);

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .IsFixedLength();
            entity.Property(e => e.RefreshToken).HasMaxLength(100);
            entity.Property(e => e.RefreshTokenExpireDate).HasColumnType("date");

            entity.HasOne(d => d.Employee).WithMany(p => p.LoginData)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginData_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

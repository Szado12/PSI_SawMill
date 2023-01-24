using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationMicroService.Models;

public partial class AuthorizationContext : DbContext
{
  private IConfiguration _configuration;
    public AuthorizationContext(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public AuthorizationContext(DbContextOptions<AuthorizationContext> options, IConfiguration configuration)
        : base(options)
    {
      _configuration = configuration;
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LoginData> LoginData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(_configuration["ConnectionString"]);

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

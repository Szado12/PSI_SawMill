using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMicroservice.Models;

public partial class EmployeeContext : DbContext
{
    public EmployeeContext()
    {
    }

    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<LoginData> LoginData { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehousesToProduct> WarehousesToProducts { get; set; }

    public virtual DbSet<WoodType> WoodTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=PSI_SawMill;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

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
                .HasConstraintName("FK__Employees__Emplo__398D8EEE");
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
            entity.Property(e => e.RefreshTokenExpireDate).HasColumnType("date");

            entity.HasOne(d => d.Employee).WithMany(p => p.LoginData)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginData_Employees");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_ProductTypes");

            entity.HasOne(d => d.WoodType).WithMany(p => p.Products)
                .HasForeignKey(d => d.WoodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_WoodTypes");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Adress).HasMaxLength(50);
        });

        modelBuilder.Entity<WarehousesToProduct>(entity =>
        {
            entity.HasOne(d => d.Product).WithMany(p => p.WarehousesToProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehousesToProducts_Products");

            entity.HasOne(d => d.Warehouse).WithMany(p => p.WarehousesToProducts)
                .HasForeignKey(d => d.WarehouseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WarehousesToProducts_Warehouses");
        });

        modelBuilder.Entity<WoodType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

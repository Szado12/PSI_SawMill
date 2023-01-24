using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProductionMicroService.Models;

public partial class ProductionContext : DbContext
{
  public ProductionContext()
    {
    }

    public ProductionContext(DbContextOptions<ProductionContext> options, IConfiguration configuration)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<LoginDatum> LoginData { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<Operation> Operations { get; set; }

    public virtual DbSet<OperationsToMachine> OperationsToMachines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ProductionDetail> ProductionDetails { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WarehousesToProduct> WarehousesToProducts { get; set; }

    public virtual DbSet<WoodType> WoodTypes { get; set; }

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
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.WorkEndDate).HasColumnType("date");
            entity.Property(e => e.WorkStartDate).HasColumnType("date");
        });

        modelBuilder.Entity<LoginDatum>(entity =>
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

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.ToTable("Machine");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.OutputProductType).WithMany(p => p.OperationOutputProductTypes)
                .HasForeignKey(d => d.OutputProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operations_ProductTypes");

            entity.HasOne(d => d.SourceProductType).WithMany(p => p.OperationSourceProductTypes)
                .HasForeignKey(d => d.SourceProductTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Operations_ProductTypes1");
        });

        modelBuilder.Entity<OperationsToMachine>(entity =>
        {
            entity.HasOne(d => d.Machine).WithMany(p => p.OperationsToMachines)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationsToMachines_Machine");

            entity.HasOne(d => d.Operation).WithMany(p => p.OperationsToMachines)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OperationsToMachines_Operations");
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

        modelBuilder.Entity<ProductionDetail>(entity =>
        {
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Machine).WithMany(p => p.ProductionDetails)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionDetails_Machine");

            entity.HasOne(d => d.Operation).WithMany(p => p.ProductionDetails)
                .HasForeignKey(d => d.OperationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionDetails_Operations");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductionDetails_Products");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Address).WithMany(p => p.Warehouses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Warehouses_Addresses");
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

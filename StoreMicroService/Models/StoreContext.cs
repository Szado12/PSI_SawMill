using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StoreMicroService.Models;

public partial class StoreContext : DbContext
{
    public StoreContext()
    {
    }

    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }
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
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(100);
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

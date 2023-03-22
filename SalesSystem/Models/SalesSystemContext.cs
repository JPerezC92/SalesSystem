using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesSystem.Models;

public partial class SalesSystemContext : DbContext
{
    public SalesSystemContext()
    {
    }

    public SalesSystemContext(DbContextOptions<SalesSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SaleDetail> SaleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SalesSystem;Trusted_Connection=True;TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("cost");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("unitPrice");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("sale");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_sale_client");
        });

        modelBuilder.Entity<SaleDetail>(entity =>
        {
            entity.ToTable("sale_detail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProduct).HasColumnName("id_product");
            entity.Property(e => e.IdSale).HasColumnName("id_sale");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("unitPrice");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_detail_product");

            entity.HasOne(d => d.IdSaleNavigation).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.IdSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sale_detail_sale");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProveedoresFimeApi.Models
{
    public partial class labdispmovilesContext : DbContext
    {
        public labdispmovilesContext()
        {
        }

        public labdispmovilesContext(DbContextOptions<labdispmovilesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<Cotizaciones> Cotizaciones { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<SolicitudArticulos> SolicitudArticulos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:labdispmoviles.database.windows.net,1433;Initial Catalog=labdispmoviles;Persist Security Info=False;User ID=user;Password=As130709$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.HasKey(e => e.ArticuloId);

                entity.Property(e => e.Descripcion).HasMaxLength(50);

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.ProveedorId)
                    .HasConstraintName("FK_Articulos_Proveedores");
            });

            modelBuilder.Entity<Cotizaciones>(entity =>
            {
                entity.HasKey(e => e.CotizacionId);

                entity.Property(e => e.CotizacionId).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Cotizaciones)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cotizaciones_Proveedores");
            });

            modelBuilder.Entity<Proveedores>(entity =>
            {
                entity.HasKey(e => e.ProveedorId);

                entity.Property(e => e.Correo).HasMaxLength(50);

                entity.Property(e => e.Direccion).HasMaxLength(150);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.Rfc)
                    .HasColumnName("RFC")
                    .HasMaxLength(13);

                entity.Property(e => e.Telefono).HasMaxLength(10);
            });

            modelBuilder.Entity<SolicitudArticulos>(entity =>
            {
                entity.HasKey(e => e.TranId);

                entity.Property(e => e.TranId).ValueGeneratedNever();

                entity.HasOne(d => d.Articulo)
                    .WithMany(p => p.SolicitudArticulos)
                    .HasForeignKey(d => d.ArticuloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_Articulos");

                entity.HasOne(d => d.Cotizacion)
                    .WithMany(p => p.SolicitudArticulos)
                    .HasForeignKey(d => d.CotizacionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_Cotizaciones");

                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.SolicitudArticulos)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Table_1_Proveedores");
            });
        }
    }
}

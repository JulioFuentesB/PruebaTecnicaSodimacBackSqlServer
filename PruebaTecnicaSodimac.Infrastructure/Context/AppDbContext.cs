// <copyright file="AppDbContext.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using ClassLibrary1.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaSodimac.Infrastructure.Context;

public partial class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Cliente> Clientes { get; set; }

	public virtual DbSet<Pedido> Pedidos { get; set; }

	public virtual DbSet<PedidoProducto> PedidoProductos { get; set; }

	public virtual DbSet<PedidoRutas> PedidoRuta { get; set; }

	public virtual DbSet<Producto> Productos { get; set; }

	public virtual DbSet<Ruta> Ruta { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Cliente>(entity =>
		{
			entity.HasKey(e => e.IdCliente).HasName("PK__Cliente__D5946642C197DB53");

			entity.ToTable("Cliente");

			entity.HasIndex(e => e.Email, "IX_Cliente_Email");

			entity.Property(e => e.Direccion).HasMaxLength(200);
			entity.Property(e => e.Email).HasMaxLength(100);
			entity.Property(e => e.Nombre).HasMaxLength(100);
			entity.Property(e => e.Telefono).HasMaxLength(20);
		});

		modelBuilder.Entity<Pedido>(entity =>
		{
			entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__9D335DC3A2D3FBED");

			entity.ToTable("Pedido");

			entity.HasIndex(e => e.Estado, "IX_Pedido_Estado");

			entity.Property(e => e.Estado)
				.HasMaxLength(20)
				.HasDefaultValue("Pendiente");
			entity.Property(e => e.FechaCreacion)
				.HasDefaultValueSql("(getdate())")
				.HasColumnType("datetime");
			entity.Property(e => e.FechaEntrega).HasColumnType("datetime");

			entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pedidos)
				.HasForeignKey(d => d.IdCliente)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_Pedido_Cliente");
		});

		modelBuilder.Entity<PedidoProducto>(entity =>
		{
			entity.HasKey(e => e.IdPedidoProducto).HasName("PK__PedidoPr__A2B1F15882A70BD6");

			entity.ToTable("PedidoProducto");

			entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoProductos)
				.HasForeignKey(d => d.IdPedido)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_PedidoProducto_Pedido");

			entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.PedidoProductos)
				.HasForeignKey(d => d.IdProducto)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_PedidoProducto_Producto");
		});

		modelBuilder.Entity<PedidoRutas>(entity =>
		{
			entity.HasKey(e => e.PedidoIdRuta).HasName("PK__PedidoRu__5B0217B88D653EE9");

			entity.Property(e => e.FechaAsignacion)
				.HasDefaultValueSql("(getdate())")
				.HasColumnType("datetime");

			entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidoRutas)
				.HasForeignKey(d => d.IdPedido)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_PedidoRuta_Pedido");

			entity.HasOne(d => d.IdRutaNavigation).WithMany(p => p.PedidoRuta)
				.HasForeignKey(d => d.IdRuta)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_PedidoRuta_Ruta");
		});

		modelBuilder.Entity<Producto>(entity =>
		{
			entity.HasKey(e => e.IdProducto).HasName("PK__Producto__0988921098866097");

			entity.ToTable("Producto");

			entity.HasIndex(e => e.Sku, "UQ__Producto__CA1ECF0D52304051").IsUnique();

			entity.Property(e => e.Descripcion).HasMaxLength(250);
			entity.Property(e => e.Nombre).HasMaxLength(100);
			entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
			entity.Property(e => e.Sku)
				.HasMaxLength(50)
				.HasColumnName("SKU");
		});

		modelBuilder.Entity<Ruta>(entity =>
		{
			entity.HasKey(e => e.IdRuta).HasName("PK__Ruta__887538FE9C19928C");

			entity.HasIndex(e => e.FechaAsignacion, "IX_Ruta_Fecha");

			entity.Property(e => e.Estado).HasMaxLength(20);
			entity.Property(e => e.FechaAsignacion).HasColumnType("datetime");
			entity.Property(e => e.FechaEstimadaEntrega).HasColumnType("datetime");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

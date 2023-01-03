﻿using Microsoft.EntityFrameworkCore;
using NorthWind.Entities.POCOEntities;

namespace NorthWind.Repositories.EFCore.DataContext
{
	public class NorthWindContext : DbContext
	{
		public NorthWindContext(
			DbContextOptions<NorthWindContext> options) :
			base(options) { }

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		protected override void OnModelCreating(
			ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
				.Property(c => c.Id)
				.HasMaxLength(5)
				.IsFixedLength();

			modelBuilder.Entity<Customer>()
				.Property(c => c.Name)
				.IsRequired()
				.HasMaxLength(40);

			modelBuilder.Entity<Product>()
				.Property(p => p.Name)
				.IsRequired()
				.HasMaxLength(40);

			modelBuilder.Entity<Order>()
				.Property(o => o.CustomerId)
				.IsRequired()
				.HasMaxLength(5)
				.IsFixedLength();

			modelBuilder.Entity<Order>()
				.Property(o => o.ShipAddress)
				.IsRequired()
				.HasMaxLength(60);

			modelBuilder.Entity<Order>()
				.Property(o => o.ShipCity)
				.IsRequired()
				.HasMaxLength(60);

			modelBuilder.Entity<Order>()
				.Property(o => o.ShipCountry)
				.IsRequired()
				.HasMaxLength(60);

			modelBuilder.Entity<Order>()
				.Property(o => o.ShipPostalCode)
				.IsRequired()
				.HasMaxLength(60);

			modelBuilder.Entity<OrderDetail>()
				.HasKey(od => new { od.OrderId, od.ProductId });

			modelBuilder.Entity<Order>()
				.HasOne<Customer>()
				.WithMany()
				.HasForeignKey(o => o.CustomerId);

			modelBuilder.Entity<OrderDetail>()
				.HasOne<Product>()
				.WithMany()
				.HasForeignKey(od => od.ProductId);


			modelBuilder.Entity<Product>()
				.HasData(
				new Product { Id = 1, Name = "Chai"},
				new Product { Id = 2, Name = "Chang"},
				new Product { Id = 3, Name = "Aniseed Syrup"}
				);

			modelBuilder.Entity<Customer>()
				.HasData(
				new Customer { Id = "ALFKI", Name = "Alfreds F."},
				new Customer { Id = "ANATR", Name = "Ana Trujillo" },
				new Customer { Id = "ANTON", Name = "Antonio Moreno" }
				);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using FlowersShop.DataAccess.Entities;


using System;
using System.Collections.Generic;

namespace FlowersShop.DataAccess
{
    public class FlowersShopDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<FlowersShop.DataAccess.Entities.FlowersShop> FlowerShops { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }

        public FlowersShopDbContext(DbContextOptions<FlowersShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);

            // Конфигурация связи один-ко-многим между Discount и UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Discounts)
                .WithMany(d => d.UserEntity)
                .HasForeignKey(u => u.DiscountId);

            // Конфигурация связи один-ко-многим между FlowersShop и Item
            modelBuilder.Entity<Item>()
                .HasOne(i => i.Flower)
                .WithMany(f => f.Items)
                .HasForeignKey(i => i.FlowerId);

            // Конфигурация связи многие-ко-многим между Item и ItemCategory
            modelBuilder.Entity<Item>()
                .HasMany(i => i.ItemsCategories)
                .WithMany(c => c.Items)
                .UsingEntity<Dictionary<string, object>>(
                    "ItemItemCategory",
                    j => j.HasOne<ItemCategory>()
                        .WithMany()
                        .HasForeignKey("ItemCategoryId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Item>()
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

            // Конфигурация связи один-ко-многим между FlowersShop и UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.FlowersShop)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FlowersShopId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

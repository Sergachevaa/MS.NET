using Microsoft.EntityFrameworkCore;
using FlowersShop.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

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
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("user_logins").HasNoKey();
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("user_tokens").HasNoKey();
            modelBuilder.Entity<IdentityRole<int>>().ToTable("user_roles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("user_roles_claims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("user_role_owners").HasNoKey();
            
            base.OnModelCreating(modelBuilder);
            // Конфигурация индекса для UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasIndex(x => x.ExternalId)
                .IsUnique(); 

            // Конфигурация связи один-ко-многим между Discount и UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Discounts)
                .WithMany(d => d.UserEntity)
                .HasForeignKey(u => u.DiscountId);

            // Конфигурация индекса для Item
            modelBuilder.Entity<Item>()
                .HasIndex(x => x.ExternalId)
                .IsUnique(); 
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

            // Конфигурация индекса для FlowersShop
            modelBuilder.Entity<FlowersShop.DataAccess.Entities.FlowersShop>()
                .HasIndex(x => x.ExternalId)
                .IsUnique(); 
            // Конфигурация связи один-ко-многим между FlowersShop и UserEntity
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.FlowersShop)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FlowersShopId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
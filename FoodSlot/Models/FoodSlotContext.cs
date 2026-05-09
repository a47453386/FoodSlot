using Microsoft.EntityFrameworkCore;
using System;

namespace FoodSlot.Models
{
    public class FoodSlotContext : DbContext
    {

        public FoodSlotContext(DbContextOptions options) : base(options)
        {
        }

        public FoodSlotContext(DbContextOptions<FoodSlotContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Range> Ranges { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        public DbSet<DrawHistory> DrawHistories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PocketList> PocketLists { get; set; }
        public DbSet<Store> Stores { get; set; }        

        public  DbSet<Geolocation> Geolocation { get; set; }
        public  DbSet<LotteryHistory> LotteryHistory { get; set; }
        public  DbSet<UserFoodSettings> UserFoodSettings { get; set; }
        public  DbSet<UserRangeSettings> UserRangeSettings { get; set; }
        public DbSet<SystemSeting> SystemSetings { get; set; }
        public DbSet<SystemSetingCcategory> SystemSetingCcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //1對多
            modelBuilder.Entity<Food>()
                .HasOne(f => f.user)
                .WithMany(u => u.Foods)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            //    //自我關聯
            modelBuilder.Entity<Food>()
                .HasOne(f => f.parentfood)
                .WithMany(f => f.children)
                .HasForeignKey(f => f.parentfoodID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Range>()
                .HasOne(r => r.user)
                    .WithMany(u => u.Ranges)
                    .HasForeignKey(r => r.userID)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Verification>()
                .HasOne(v => v.user)
                .WithMany(u => u.Verifications)
                .HasForeignKey(v => v.userID)
                .OnDelete(DeleteBehavior.Restrict);


           

            // --- DrawHistory 設定 ---
            modelBuilder.Entity<DrawHistory>()
                .HasOne(d => d.user)
                .WithMany(DrawHistories)
                .HasForeignKey(d => d.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DrawHistory>()
                .HasOne(d => d.stores)
                .WithMany(DrawHistories)
                .HasForeignKey(d => d.storeID)
                .OnDelete(DeleteBehavior.NoAction); 

            // --- PocketList 設定 ---
            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.user)
                .WithMany(PocketLists)
                .HasForeignKey(p => p.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.stores)
                .WithMany(PocketLists)
                .HasForeignKey(p => p.storeID)
                .OnDelete(DeleteBehavior.NoAction);

            // --- Feedback 設定 ---
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.user)
                .WithMany(Feedbacks)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Store 設定 ---
            modelBuilder.Entity<Store>()
                .HasOne(f => f.food)
                .WithMany(Stores)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.Cascade);



            // --- SystemSeting設定 ---

            modelBuilder.Entity<SystemSetingCcategory>()
                .HasOne(f => f.user)
                .WithMany(SystemSetingCcategories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SystemSeting>()
                .HasOne(f => f.user)
                .WithMany(SystemSetings)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SystemSeting>()
                .HasOne(f => f.systemSetingCcategories)
                .WithMany(SystemSetings)
                .HasForeignKey(f => f.categoryID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

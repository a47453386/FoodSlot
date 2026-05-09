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
        public DbSet<UserRange> UserRanges { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        public DbSet<DrawHistory> DrawHistories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PocketList> PocketLists { get; set; }
        public DbSet<Store> Stores { get; set; }        

        public  DbSet<Geolocation> Geolocations { get; set; }
        public  DbSet<LotteryHistory> LotteryHistories { get; set; }
        public  DbSet<UserFoodSettings> UserFoodSettings { get; set; }
        public  DbSet<UserRangeSettings> UserRangeSettings { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<SystemSettingCategory> SystemSettingCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //1對多
            modelBuilder.Entity<Food>()
                .HasOne(f => f.User)
                .WithMany(u => u.Foods)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            //    //自我關聯
            modelBuilder.Entity<Food>()
                .HasOne(f => f.Parentfood)
                .WithMany(f => f.Childrens)
                .HasForeignKey(f => f.parentfoodID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRange>()
                .HasOne(r => r.User)
                    .WithMany(u => u.UserRanges)
                    .HasForeignKey(r => r.userID)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Verification>()
                .HasOne(v => v.User)
                .WithMany(u => u.Verifications)
                .HasForeignKey(v => v.userID)
                .OnDelete(DeleteBehavior.Restrict);


           

            // --- DrawHistory 設定 ---
            modelBuilder.Entity<DrawHistory>()
                .HasOne(d => d.User)
                .WithMany(u => u.DrawHistories)
                .HasForeignKey(d => d.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DrawHistory>()
                .HasOne(d => d.Stores)
                .WithMany(u => u.DrawHistories)
                .HasForeignKey(d => d.storeID)
                .OnDelete(DeleteBehavior.Restrict); 

            // --- PocketList 設定 ---
            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.User)
                .WithMany(u => u.PocketLists)
                .HasForeignKey(p => p.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.Store)
                .WithMany(u => u.PocketLists)
                .HasForeignKey(p => p.storeID)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Feedback 設定 ---
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany(u => u.Feedbacks)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Store 設定 ---
            modelBuilder.Entity<Store>()
                .HasOne(f => f.Food)
                .WithMany(u => u.Stores)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.Restrict);



            // --- SystemSeting設定 ---

            modelBuilder.Entity<SystemSettingCategory>()
                .HasOne(f => f.User)
                .WithMany(u => u.SystemSettingCategories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemSetting>()
                .HasOne(f => f.User)
                .WithMany(u => u.SystemSettings)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemSetting>()
                .HasOne(f => f.SystemSettingCategories)
                .WithMany(u => u.SystemSettings)
                .HasForeignKey(f => f.categoryID)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Geolocation>()
                .HasOne(f => f.User)
                .WithMany(u => u.Geolocations)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.User)
                .WithMany(u => u.LotteryHistories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.Food)
                .WithMany(u => u.LotteryHistories)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.Restrict);


            // --- 使用者種類設定 (UserFoodSettings) 關聯配置 ---
            modelBuilder.Entity<UserFoodSettings>(entity =>
            {
                entity.ToTable("UserFoodSettings");

                // 定義複合主鍵 (Composite Key)
                entity.HasKey(e => new { e.userID, e.foodID });

                // 關聯到 User 表
                entity.HasOne<User>()
                    .WithMany(u => u.UserFoodSettings)
                    .HasForeignKey(e => e.userID)
                    .OnDelete(DeleteBehavior.Restrict);

                // 關聯到 Food 表
                entity.HasOne<Food>()
                    .WithMany(u => u.UserFoodSettings)
                    .HasForeignKey(e => e.foodID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // --- 使用者範圍設定 (UserRangeSettings) 關聯配置 ---
            modelBuilder.Entity<UserRangeSettings>(entity =>
            {
                entity.ToTable("UserRangeSettings");

                // 定義複合主鍵
                entity.HasKey(e => new { e.userID, e.rangeID });

                // 關聯到 User 表
                entity.HasOne<User>()
                    .WithMany(u => u.UserRangeSettings)
                    .HasForeignKey(e => e.userID)
                    .OnDelete(DeleteBehavior.Restrict);

                // 關聯到 Range 表
                entity.HasOne<UserRange>() // 註：Range 通常是 C# 關鍵字，建議類別名取 RangeEntity
                    .WithMany(u => u.UserRangeSettings)
                    .HasForeignKey(e => e.rangeID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

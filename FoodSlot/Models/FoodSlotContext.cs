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
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DrawHistory>()
                .HasOne(d => d.stores)
                .WithMany(DrawHistories)
                .HasForeignKey(d => d.storeID)
                .OnDelete(DeleteBehavior.Restrict); 

            // --- PocketList 設定 ---
            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.user)
                .WithMany(PocketLists)
                .HasForeignKey(p => p.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PocketList>()
                .HasOne(p => p.stores)
                .WithMany(PocketLists)
                .HasForeignKey(p => p.storeID)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Feedback 設定 ---
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.user)
                .WithMany(Feedbacks)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Store 設定 ---
            modelBuilder.Entity<Store>()
                .HasOne(f => f.food)
                .WithMany(Stores)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.Restrict);



            // --- SystemSeting設定 ---

            modelBuilder.Entity<SystemSetingCcategory>()
                .HasOne(f => f.user)
                .WithMany(SystemSetingCcategories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemSeting>()
                .HasOne(f => f.user)
                .WithMany(SystemSetings)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SystemSeting>()
                .HasOne(f => f.systemSetingCcategories)
                .WithMany(SystemSetings)
                .HasForeignKey(f => f.categoryID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Geolocation>()
                .HasOne(f => f.user)
                .WithMany(Geolocations)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.user)
                .WithMany(LotteryHistories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.food)
                .WithMany(LotteryHistories)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.Restrict);


            // --- 使用者種類設定 (UserFoodSettings) 關聯配置 ---
            modelBuilder.Entity<UserFoodSettings>(entity =>
            {
                entity.ToTable("UserFoodSettings");

                // 定義複合主鍵 (Composite Key)
                entity.HasKey(e => new { e.UserId, e.FoodId });

                // 關聯到 User 表
                entity.HasOne<User>()
                    .WithMany(UserFoodSettings)
                    .HasForeignKey(e => e.userID)
                    .OnDelete(DeleteBehavior.Restrict);

                // 關聯到 Food 表
                entity.HasOne<Food>()
                    .WithMany(UserFoodSettings)
                    .HasForeignKey(e => e.FoodId)
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
                    .WithMany(UserRangeSettings)
                    .HasForeignKey(e => e.userID)
                    .OnDelete(DeleteBehavior.Restrict);

                // 關聯到 Range 表
                entity.HasOne<RangeEntity>() // 註：Range 通常是 C# 關鍵字，建議類別名取 RangeEntity
                    .WithMany(UserRangeSettings)
                    .HasForeignKey(e => e.rangeID)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

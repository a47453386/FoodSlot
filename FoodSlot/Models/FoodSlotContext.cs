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
                .OnDelete(DeleteBehavior.NoAction);

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


            modelBuilder.Entity<Geolocation>()
                .HasOne(f => f.user)
                .WithMany(Geolocations)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.user)
                .WithMany(LotteryHistories)
                .HasForeignKey(f => f.userID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LotteryHistory>()
                .HasOne(f => f.food)
                .WithMany(LotteryHistories)
                .HasForeignKey(f => f.foodID)
                .OnDelete(DeleteBehavior.NoAction);

            // --- 使用者種類設定 (UserFoodSettings) 關聯配置 ---
            modelBuilder.Entity<UserFoodSettings>(entity =>
            {
                entity.ToTable("UserFoodSettings");

                // 定義複合主鍵 (Composite Key)
                entity.HasKey(e => new { e.UserId, e.FoodId });

                // 關聯到 User 表
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 關聯到 Food 表
                entity.HasOne<Food>()
                    .WithMany()
                    .HasForeignKey(e => e.FoodId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // --- 使用者範圍設定 (UserRangeSettings) 關聯配置 ---
            modelBuilder.Entity<UserRangeSettings>(entity =>
            {
                entity.ToTable("UserRangeSettings");

                // 定義複合主鍵
                entity.HasKey(e => new { e.UserId, e.RangeId });

                // 關聯到 User 表
                entity.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // 關聯到 Range 表
                entity.HasOne<RangeEntity>() // 註：Range 通常是 C# 關鍵字，建議類別名取 RangeEntity
                    .WithMany()
                    .HasForeignKey(e => e.RangeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

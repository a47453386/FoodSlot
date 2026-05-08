using Microsoft.EntityFrameworkCore;

namespace FoodSlot.Models
{
    public class FoodSlotContext : DbContext
    {
        public FoodSlotContext(DbContextOptions<FoodSlotContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Range> Ranges { get; set; }
        public DbSet<Verification> Verifications { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //1對多
        //    modelBuilder.Entity<Food>()
        //        .HasOne(f => f.user)
        //        .WithMany(u => u.Foods)
        //        .HasForeignKey(f => f.userID)
        //        .OnDelete(DeleteBehavior.Restrict);
        //    //自我關聯
        //    modelBuilder.Entity<Food>()
        //        .HasOne(f => f.parentfood)
        //        .WithMany(f => f.children)
        //        .HasForeignKey(f => f.parentfoodID)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Range>()
        //        .HasOne(r => r.user)
        //        .WithMany(u => u.Ranges)
        //        .HasForeignKey(r => r.userID)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    modelBuilder.Entity<Verification>()
        //        .HasOne(v => v.user)
        //        .WithMany(u => u.Verifications)
        //        .HasForeignKey(v => v.userID)
        //        .OnDelete(DeleteBehavior.Restrict);

        //}


    }
}

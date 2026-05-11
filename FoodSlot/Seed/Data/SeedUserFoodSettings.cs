    using FoodSlot.Models;
    using Microsoft.EntityFrameworkCore;

namespace FoodSlot.Seed.Data
{
    public class SeedUserFoodSettings
    {
        private readonly FoodSlotContext _context;

        public SeedUserFoodSettings(FoodSlotContext context)
        {
            _context = context;
        }
        public void Run()
        {
            if (_context.UserFoodSettings.Any()) return;

            _context.UserFoodSettings.AddRange(
               new UserFoodSettings
               {
                   userID = 3,
                   foodID = 1
               },
                 new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 2
                 },
                 new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 3
                 },
                 new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 4
                 },
                 new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 5
                 }, new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 6
                 }, new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 7
                 }, new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 8
                 }, new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 9
                 }, new UserFoodSettings
                 {
                     userID = 3,
                     foodID = 10
                 }
            );
            _context.SaveChanges();
        }
    }
}
        
 

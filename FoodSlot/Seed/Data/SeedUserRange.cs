using FoodSlot.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodSlot.Seed.Data
{
    public class SeedUserRange
    {
        private readonly FoodSlotContext _context;

        public SeedUserRange(FoodSlotContext context)
        {
            _context = context;
        }
        public void Run()
        {
            if (_context.UserRanges.Any()) return; 

            _context.UserRanges.AddRange(
               new UserRange
               {
                   rangeID = 1,
                   radius = 500,
                   createTime = DateTime.Now,
                   updateTime = null
               },
                    new UserRange
                    {
                        rangeID = 2,
                        radius = 1000,
                        createTime = DateTime.Now,
                        updateTime = null
                    },
                    new UserRange
                    {
                        rangeID = 3,
                        radius = 3000,
                        createTime = DateTime.Now,
                        updateTime = null
                    }
            );
            _context.SaveChanges();
        }

    }
}

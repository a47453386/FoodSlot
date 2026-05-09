using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class UserFoodSettings
    {
        public int userID { get; set; }
        public virtual List<User> Users { get; set; } = new List<User>();
        public int foodID { get; set; }
        public virtual List<Food> Foods { get; set; } = new List<Food>();
    }
}

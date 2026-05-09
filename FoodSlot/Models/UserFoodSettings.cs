using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class UserFoodSettings
    {
        public int userID { get; set; }
        public virtual User Users { get; set; }
        public int foodID { get; set; }
        public virtual Food Foods { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class UserFoodSettings
    {
        [Key]
        public int foodSettingsID { get; set; }       

        public int userID { get; set; }
        public virtual User Users { get; set; }

        public int foodID { get; set; }
        public virtual Food Foods { get; set; }

    }
}

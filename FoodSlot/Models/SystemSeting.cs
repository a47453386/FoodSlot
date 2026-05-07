using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class SystemSeting
    {
        [Key]
        public int systemSetingID { get; set; }

        public int maxHistory { get; set; }
                
        public bool toggleRegistration { get; set; }

        public int userID { get; set; }
        public virtual User? Users { get; set; }

    }
}

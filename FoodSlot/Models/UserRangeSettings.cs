using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class UserRangeSettings
    {
        public int userID { get; set; }
        public virtual List<User> Users { get; set; } = new List<User>();
        public int rangeID { get; set; }
        public virtual List<UserRange> Ranges { get; set; } = new List<UserRange>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class Range
    {
        [Key]
        public int rangeID { get; set; }
        public int radius { get; set; }
        public DateTime createTime { get; set; }
        public DateTime? updateTime { get; set; }
        public int userID { get; set; }

        public virtual User user { get; set; } = new User();
    }
}

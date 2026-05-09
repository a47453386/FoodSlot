using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class Range
    {
        [Key]
        public int rangeID { get; set; }
        public int radius { get; set; }
        public DateTime createTime { get; set; } = DateTime.Now;
        public DateTime? updateTime { get; set; }

        public int userID { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual List<UserRangeSettings> UserRangeSettings { get; set; } = new List<UserRangeSettings>();
    }
}

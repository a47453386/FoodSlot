using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class DrawHistory
    {
        [Key]
        public int drawHistoryID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int userID { get; set; }
        public virtual User user { get; set; }
        public int storeID { get; set; }
        public virtual Store? store { get; set; }
    }
}

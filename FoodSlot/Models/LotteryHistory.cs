using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class LotteryHistory
    {
        [Key]
        public int LotteryHistoryID { get; set; }
        [DataType(DataType.DateTime)] 
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public virtual User Users { get; set; }
        public int FoodID { get; set; }
        public virtual Food Foods { get; set; }
    }
}

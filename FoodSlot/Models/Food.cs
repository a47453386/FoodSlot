using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class Food
    {
        [Key]
        public int foodID { get; set; }
        [StringLength(50)]
        public string foodname { get; set; } = null!;
        [StringLength(36)]
        public string photo { get; set; } = null!;
        public DateTime createTime { get; set; }
        public DateTime? updateTime { get; set; }
        public int? parentfoodID { get; set; }
        public int userID { get; set; }

        public virtual Food? parentfood { get; set; }
        public virtual List<Food> children { get; set; } = new List<Food>();
        public virtual User user { get; set; } = new User();
    }
}

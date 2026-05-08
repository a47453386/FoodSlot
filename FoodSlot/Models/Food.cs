using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class Food
    {
        [Key]
        public int foodID { get; set; }

        [MaxLength(50)]
        public string foodname { get; set; } = null!;
        [MaxLength(36)]
        public string photo { get; set; } = null!;
        public DateTime createTime { get; set; } = DateTime.Now;
        public DateTime? updateTime { get; set; }

        public int? parentfoodID { get; set; }
        public virtual Food? parentfood { get; set; }
        public virtual List<Food> childrens { get; set; } = new List<Food>();
        public int userID { get; set; }
        public virtual User user { get; set; } = null!;
    }
}

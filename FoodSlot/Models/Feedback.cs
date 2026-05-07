using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class Feedback
    {
        [Key]
        public int feedbackID { get; set; }

        [DataType(DataType.MultilineText)]
        public string message { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime createTime { get; set; } = DateTime.Now;
        public bool state { get; set; } 

        public int userID { get; set; }
        public virtual User user { get; set; }
    }
}

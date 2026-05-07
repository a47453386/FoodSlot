using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class PocketList
    {
        [Key]
        public int pocketListID { get; set; }

        [DataType(DataType.MultilineText)]
        public string comment { get; set; } = null!;

        [DataType(DataType.DateTime)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int userID { get; set; }
        public virtual user User { get; set; }
        public int storeID { get; set; }
        public virtual Store store { get; set; }
    }
}

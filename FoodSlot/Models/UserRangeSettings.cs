using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class UserRangeSettings
    {
        [Key]
        public int RangeSettingsID { get; set; }

        public int userID { get; set; }
        public virtual User Users { get; set; }

        public int rangeID { get; set; }
        public virtual Range Ranges { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace FoodSlot.Models
{
    public class SystemSeting
    {
        [Key]
        public int systemSetingID { get; set; }

        [MaxLength(100)]
        public string settingName { get; set; } = null!;

        [MaxLength(100)]
        public string settingValue { get; set; } = null!;

        [MaxLength(20)]
        public string valueType { get; set; } = null!;

        [MaxLength(200)]
        public string? description { get; set; }
                
    

        public int userID { get; set; }
        public virtual User user { get; set; } = null!;

        public int categoryID { get; set; }
        public virtual SystemSetingCcategory? systemSetingCcategories { get; set; } = null!;


    }
}

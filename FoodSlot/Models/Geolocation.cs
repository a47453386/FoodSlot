using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodSlot.Models
{
    public class Geolocation
    {
        [Key]
        public int geolocationID { get; set; }


        [MaxLength(50)]
        public string geolocationName { get; set; }
        public double geolng { get; set; }
        public double geolat { get; set; }
        public int userID { get; set; }
        public virtual User Users { get; set; }
    }
    }


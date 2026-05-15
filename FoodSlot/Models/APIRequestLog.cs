using System.ComponentModel.DataAnnotations;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FoodSlot.Models
{
    public class APIRequestLog
    {
        [Key]
        public long apiRequestLogID { get; set; }

        [MaxLength(100)]
        public string apiName { get; set; } = null!;
        public int totalRequests { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime updatedAt { get; set; } = DateTime.Now;

    }
}

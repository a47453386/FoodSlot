namespace FoodSlot.Models
{
    public class Verification
    {
        public int verificationID { get; set; }
        public int code { get; set; }
        public DateTime createTime { get; set; }
        public int userID { get; set; }
        public virtual User user { get; set; } = new User();
    }
}

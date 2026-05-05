namespace FoodSlot.Models
{
    public class User
    {
        public int userID { get; set; }
        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public string email { get; set; } = null!;
        public bool isAdmin { get; set; } = false;
        public DateTime createTime { get; set; }
        public DateTime lastLoginTime { get; set; }

        public virtual List<Food> Foods { get; set; } = new List<Food>();
        public virtual List<Range> Ranges { get; set; } = new List<Range>();
        public virtual List<Verification> Verifications { get; set; } = new List<Verification>();
        public virtual List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public virtual List<PocketList> PocketLists { get; set; } = new List<PocketList>();
        public virtual List<DrawHistory> DrawHistories { get; set; } = new List<DrawHistory>();
        public virtual List<Geolocation> Geolocations { get; set; } = new List<Geolocation>();
        public virtual List<LotteryHistory> LotteryHistories { get; set; } = new List<LotteryHistory>();
        public virtual List<UserFoodSettings> UserFoodSettings { get; set; } = new List<UserFoodSettings>();
        public virtual List<UserRangeSettings> UserRangeSettings { get; set; } = new List<UserRangeSettings>();
    }
}

namespace FoodSlot.DTOs
{
    public class StoreDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public double Rating { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}

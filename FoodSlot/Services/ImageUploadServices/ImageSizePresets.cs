namespace FoodSlot.Services.ImageUploadService.ImageUploadService
{
    public static class ImageSizePresets
    {
        public static List<ImageSizeOption> Food =>
            new()
            {
                new ImageSizeOption {Width = 256, Height = 256 }
            };
    }
}
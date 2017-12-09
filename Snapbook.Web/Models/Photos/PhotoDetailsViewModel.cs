namespace Snapbook.Web.Models.Photos
{
    using Data.Models;
    using Services.Models.Photo;

    public class PhotoDetailsViewModel
    {
        public PhotoDetailsServiceModel Photo { get; set; }
        
        public bool CanLike { get; set; }

        public User User { get; set; }
    }
}

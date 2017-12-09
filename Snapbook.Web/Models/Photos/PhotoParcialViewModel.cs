namespace Snapbook.Web.Models.Photos
{
    using Data.Models;
    using Services.Models.Photo;

    public class PhotoParcialViewModel
    {
        public PhotoHomeServiceModel Photo { get; set; }

        public User User { get; set; }
    }
}

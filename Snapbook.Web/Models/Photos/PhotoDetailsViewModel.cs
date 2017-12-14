namespace Snapbook.Web.Models.Photos
{
    using System.Collections.Generic;
    using Data.Models;
    using Services.Models.Photo;

    public class PhotoDetailsViewModel
    {
        public PhotoDetailsServiceModel Photo { get; set; }
        
        public IEnumerable<PhotoRelatedServiceModel> RelatedPhotos { get; set; }

        public bool CanLike { get; set; }

        public bool CanSave { get; set; }

        public User User { get; set; }
    }
}

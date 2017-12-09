namespace Snapbook.Web.Models.Photos
{
    using Data.Models;
    using Services.Models.Photo;
    using System.Collections.Generic;

    public class PhotoHomeListingViewModel
    {
        public IEnumerable<PhotoHomeServiceModel> Photos { get; set; }

        public User User { get; set; }
    }
}

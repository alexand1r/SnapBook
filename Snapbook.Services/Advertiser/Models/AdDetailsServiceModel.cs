namespace Snapbook.Services.Advertiser.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using AutoMapper;
    using Snapbook.Services.Models.Photo;

    public class AdDetailsServiceModel : IMapFrom<Ad>, IHaveCustomMapping
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string AdProfilePicUrl { get; set; }
        
        public string Description { get; set; }

        public string Website { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public IEnumerable<PhotoListingServiceModel> Photos { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Ad, AdDetailsServiceModel>()
                .ForMember(ad => ad.User, cfg => cfg.MapFrom(a => a.User.UserName));
    }
}

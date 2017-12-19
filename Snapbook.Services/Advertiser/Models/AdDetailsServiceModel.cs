namespace Snapbook.Services.Advertiser.Models
{
    using Common.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    using AutoMapper;
    using Snapbook.Services.Models.Photo;

    public class AdDetailsServiceModel : AdEditServiceModel, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string User { get; set; }

        public IEnumerable<PhotoListingServiceModel> Photos { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Ad, AdDetailsServiceModel>()
                .ForMember(ad => ad.User, cfg => cfg.MapFrom(a => a.User.UserName));
    }
}

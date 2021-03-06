﻿namespace Snapbook.Services.Advertiser.Models
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Services.Models.Photo;
    using System.Collections.Generic;

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

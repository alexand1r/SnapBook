﻿namespace Snapbook.Services.Advertiser.Models
{
    using Common.Mapping;
    using Data.Models;

    public class AdEditServiceModel : IMapFrom<Ad>
    {
        public string Name { get; set; }

        public string AdProfilePicUrl { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public string UserId { get; set; }
    }
}

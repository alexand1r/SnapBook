﻿namespace Snapbook.Services.Admin.Models.Ad
{
    using Common.Mapping;
    using Data.Models;

    public class AdEditServiceModel : IMapFrom<Ad>
    {
        public string Name { get; set; }

        public string AdProfilePicUrl { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }
    }
}

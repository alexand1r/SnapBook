namespace Snapbook.Services.Models.Photo
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Services.Models.Tag;
    using System.Collections.Generic;

    public class EditPhotoServiceModel : IMapFrom<Photo>, IHaveCustomMapping
    {
        public string Description { get; set; }

        public string Location { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string AlbumUserId { get; set; }

        public string AdUserId { get; set; }

        public IEnumerable<TagListingServiceModel> Tags { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, EditPhotoServiceModel>()
                .ForMember(ep => ep.AlbumUserId, cfg => cfg.MapFrom(p => p.Album.UserId))
                .ForMember(ep => ep.AdUserId, cfg => cfg.MapFrom(p => p.Ad.UserId));
    }
}

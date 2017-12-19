namespace Snapbook.Services.Models.Photo
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class PhotoListingServiceModel : PhotoRelatedServiceModel, IHaveCustomMapping
    {
        //public int Id { get; set; }

        //public string ImageUrl { get; set; }

        public DateTime PublishDate { get; set; }

        public int Likes { get; set; }

        public int Comments { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Photo, PhotoListingServiceModel>()
                .ForMember(p => p.Likes, cfg => cfg.MapFrom(p => p.Likers.Count))
                .ForMember(p => p.Comments, cfg => cfg.MapFrom(p => p.Comments.Count));
    }
}

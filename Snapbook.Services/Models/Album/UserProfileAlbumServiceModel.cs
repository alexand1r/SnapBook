namespace Snapbook.Services.Models.Album
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using System;

    public class UserProfileAlbumServiceModel : IMapFrom<Album>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public DateTime PublishDate { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Album, UserProfileAlbumServiceModel>()
                .ForMember(upa => upa.CategoryName, cfg => cfg.MapFrom(a => a.Category.Name));
    }
}

namespace Snapbook.Services.Models.Album
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;
    using Models.Photo;
    using System.Collections.Generic;

    public class AlbumDetailsServiceModel : IMapFrom<Album>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public string CategoryName { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public IEnumerable<PhotoListingServiceModel> Photos { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Album, AlbumDetailsServiceModel>()
                .ForMember(upa => upa.CategoryName, cfg => cfg.MapFrom(a => a.Category.Name))
                .ForMember(upa => upa.Username, cfg => cfg.MapFrom(a => a.User.UserName));
    }
}

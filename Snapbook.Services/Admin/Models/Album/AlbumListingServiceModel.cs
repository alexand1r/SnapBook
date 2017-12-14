namespace Snapbook.Services.Admin.Models.Album
{
    using AutoMapper;
    using Common.Mapping;
    using Data.Models;

    public class AlbumListingServiceModel : IMapFrom<Album>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public void ConfigureMapping(Profile mapper)
            => mapper
                .CreateMap<Album, AlbumListingServiceModel>()
                .ForMember(al => al.Category, cfg => cfg.MapFrom(a => a.Category.Name));
    }
}

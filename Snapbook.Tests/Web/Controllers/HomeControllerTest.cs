namespace Snapbook.Tests.Web.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Snapbook.Services;
    using Snapbook.Services.Models.Ad;
    using Snapbook.Services.Models.Album;
    using Snapbook.Services.Models.Photo;
    using Snapbook.Services.Models.User;
    using Snapbook.Web.Controllers;
    using Snapbook.Web.Models.Home;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void SearchShouldReturnViewWithValidModel()
        {
            // Arrange
            var controller = new HomeController(null, null, null, null, null);

            // Act
            var result = controller.Search(new SearchViewModel
            {
                Results = null
            });

            // Assert
            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().Model.Should().BeOfType<SearchViewModel>();
        }

        [Fact]
        public async Task SearchShouldReturnNoResultsWithNoCriteria()
        {
            // Arrange
            var controller = new HomeController(null, null, null, null, null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchInPhotos = false,
                SearchInAlbums = false,
                SearchInAds = false,
                SearchInUsers = false
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();
            
            searchViewModel.Photos.Should().BeEmpty();
            searchViewModel.Albums.Should().BeEmpty();
            searchViewModel.Ads.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenUsersAreFiltered()
        {
            // Arrange
            const string searchText = "Text";
            var userService = this.UserServiceMock();

            var controller = new HomeController(null, null, userService.Object, null, null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchText = searchText,
                SearchInUsers = true,
                SearchInAds = false,
                SearchInAlbums = false,
                SearchInPhotos = false
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();

            searchViewModel.Users.Should().Match(c => c.As<List<UserListingServiceModel>>().Count == 1);
            searchViewModel.Users.First().Should().Match(c => c.As<UserListingServiceModel>().UserName == "user");
            searchViewModel.Photos.Should().BeEmpty();
            searchViewModel.Albums.Should().BeEmpty();
            searchViewModel.Ads.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenAdsAreFiltered()
        {
            // Arrange
            const string searchText = "Text";

            var adService = this.AdServiceMock();

            var controller = new HomeController(null, null, null, adService.Object, null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchText = searchText,
                SearchInUsers = false,
                SearchInAds = true,
                SearchInAlbums = false,
                SearchInPhotos = false
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();

            searchViewModel.Ads.Should().Match(c => c.As<List<AdListingServiceModel>>().Count == 1);
            searchViewModel.Ads.First().Should().Match(c => c.As<AdListingServiceModel>().Id == 1);
            searchViewModel.Photos.Should().BeEmpty();
            searchViewModel.Albums.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenAlbumsAreFiltered()
        {
            // Arrange
            const string searchText = "Text";

            var albumService = this.AlbumServiceMock();

            var controller = new HomeController(null, albumService.Object, null, null, null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchText = searchText,
                SearchInUsers = false,
                SearchInAds = false,
                SearchInAlbums = true,
                SearchInPhotos = false
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();

            searchViewModel.Albums.Should().Match(c => c.As<List<AlbumListingServiceModel>>().Count == 1);
            searchViewModel.Albums.First().Should().Match(c => c.As<AlbumListingServiceModel>().Id == 1);
            searchViewModel.Photos.Should().BeEmpty();
            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.Ads.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenPhotosAreFiltered()
        {
            // Arrange
            const string searchText = "Text";

            var photoService = this.PhotoServiceMock();

            var controller = new HomeController(photoService.Object, null, null, null, null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchText = searchText,
                SearchInUsers = false,
                SearchInAds = false,
                SearchInAlbums = false,
                SearchInPhotos = true
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();

            searchViewModel.Photos.Should().Match(c => c.As<List<PhotoListingServiceModel>>().Count == 1);
            searchViewModel.Photos.First().Should().Match(c => c.As<PhotoListingServiceModel>().Id == 1);
            searchViewModel.Users.Should().BeEmpty();
            searchViewModel.Albums.Should().BeEmpty();
            searchViewModel.Ads.Should().BeEmpty();
        }

        [Fact]
        public async Task SearchShouldReturnViewWithValidModelWhenAllIsFiltered()
        {
            // Arrange
            const string searchText = "Text";

            var userService = this.UserServiceMock();
            var photoService = this.PhotoServiceMock();
            var albumService = this.AlbumServiceMock();
            var adService = this.AdServiceMock();

            var controller = new HomeController(
                photoService.Object,
                albumService.Object,
                userService.Object,
                adService.Object,
                null);

            // Act
            var result = await controller.SearchResults(new SearchViewModel
            {
                SearchText = searchText,
                SearchInUsers = true,
                SearchInAds = true,
                SearchInAlbums = true,
                SearchInPhotos = true
            });

            // Assert
            result.Should().BeOfType<PartialViewResult>();

            result.As<PartialViewResult>().Model.Should().BeOfType<SearchResultsViewModel>();

            var searchViewModel = result.As<PartialViewResult>().Model.As<SearchResultsViewModel>();

            searchViewModel.Users.Should().Match(c => c.As<List<UserListingServiceModel>>().Count == 1);
            searchViewModel.Users.First().Should().Match(c => c.As<UserListingServiceModel>().UserName == "user");
            searchViewModel.Photos.Should().Match(c => c.As<List<PhotoListingServiceModel>>().Count == 1);
            searchViewModel.Photos.First().Should().Match(c => c.As<PhotoListingServiceModel>().Id == 1);
            searchViewModel.Albums.Should().Match(c => c.As<List<AlbumListingServiceModel>>().Count == 1);
            searchViewModel.Albums.First().Should().Match(c => c.As<AlbumListingServiceModel>().Id == 1);
            searchViewModel.Ads.Should().Match(c => c.As<List<AdListingServiceModel>>().Count == 1);
            searchViewModel.Ads.First().Should().Match(c => c.As<AdListingServiceModel>().Id == 1);
        }

        private Mock<IUserService> UserServiceMock()
        {
            var userService = new Mock<IUserService>();
            userService
                .Setup(c => c.Find(It.IsAny<string>()))
                .ReturnsAsync(new List<UserListingServiceModel>
                {
                    new UserListingServiceModel { UserName = "user" }
                });

            return userService;
        }

        private Mock<IPhotoService> PhotoServiceMock()
        {
            var photoService = new Mock<IPhotoService>();
            photoService
                .Setup(c => c.Find(It.IsAny<string>()))
                .ReturnsAsync(new List<PhotoListingServiceModel>
                {
                    new PhotoListingServiceModel { Id = 1 }
                });

            return photoService;
        }

        private Mock<IAdService> AdServiceMock()
        {
            var adService = new Mock<IAdService>();
            adService
                .Setup(c => c.FindForListing(It.IsAny<string>()))
                .ReturnsAsync(new List<AdListingServiceModel>
                {
                    new AdListingServiceModel { Id = 1 }
                });

            return adService;
        }

        private Mock<IAlbumService> AlbumServiceMock()
        {
            var albumService = new Mock<IAlbumService>();
            albumService
                .Setup(c => c.Find(It.IsAny<string>()))
                .ReturnsAsync(new List<AlbumListingServiceModel>
                {
                    new AlbumListingServiceModel { Id = 1 }
                });

            return albumService;
        }
    }
}

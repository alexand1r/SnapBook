﻿namespace Snapbook.Tests.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Snapbook.Services;
    using Snapbook.Web.Controllers;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Snapbook.Services.Models.Album;
    using Xunit;

    public class AlbumsControllerTest
    {
        [Fact]
        public async Task AlbumDetailsShouldReturnNotFoundWithInvalidId()
        {
            // Arrange
            var albumService = this.AlbumServiceMock();

            var controller = new AlbumsController(albumService.Object);

            // Act
            var result = await controller.Details(4);

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task AlbumsDetailsShouldReturnViewWithCorrectModelWithValidId()
        {
            // Arrange
            var albumService = this.AlbumServiceMock();

            var controller = new AlbumsController(albumService.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            result.Should().BeOfType<ViewResult>();

            var albumModel = result.As<ViewResult>().Model.As<AlbumDetailsServiceModel>();

            albumModel.Should().Match(c => c.As<AlbumDetailsServiceModel>().Id == 1);
        }

        private Mock<IAlbumService> AlbumServiceMock()
        {
            var albumService = new Mock<IAlbumService>();
            albumService
                .Setup(u => u.Details(It.Is<int>(id => id == 1)))
                .ReturnsAsync(new AlbumDetailsServiceModel { Id = 1 });

            return albumService;
        }
    }
}
namespace Snapbook.Tests.Services
{
    using Data;
    using Data.Models;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using Snapbook.Services.Implementations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Snapbook.Services;
    using Snapbook.Services.Models.Album;
    using Xunit;

    public class AlbumServiceTest
    {
        private readonly SnapbookDbContext db;

        public AlbumServiceTest()
        {
            this.db = Tests.GetDatabase();
            this.PopulateDb();
        }

        [Fact]
        public async Task FindShouldReturnNoResultOnNullSearchText()
        {
            // Arrange
            Tests.Initialize();
            var albumService = new AlbumService(this.db);

            // Act
            var result = await albumService.Find(null);

            // Assert
            result
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task FindShouldReturnCorrectResultsWithFilterAndOrder()
        {
            // Arrange
            Tests.Initialize();
            var albumService = new AlbumService(this.db);

            // Act
            var result = await albumService.Find("t");

            // Assert
            result
                .Should()
                .BeOfType<List<AlbumListingServiceModel>>();

            result.Should()
                .Match(r =>
                    r.ElementAt(0).Id == 3
                    && r.ElementAt(1).Id == 1)
                .And
                .HaveCount(2);
        }

        [Fact]
        public async Task DetailsShouldReturnCorrectResults()
        {
            // Arrange
            Tests.Initialize();
            var albumService = new AlbumService(this.db);

            // Act
            var result = await albumService.Details(1);

            // Assert
            result
                .Should()
                .BeOfType<AlbumDetailsServiceModel>()
                .Subject
                .Description
                .Should()
                .Be("First");
        }

        private async void PopulateDb()
        {
            var firstCategory = new Category { Id = 1, Name = "asd" };
            var firstAlbum = new Album { Id = 1, Title = "First", Description = "First", CategoryId = 1 };
            var secondAlbum = new Album { Id = 2, Title = "Second", Description = "Second", CategoryId = 1 };
            var thirdAlbum = new Album { Id = 3, Title = "Third", Description = "Third", CategoryId = 1 };

            this.db.Categories.Add(firstCategory);
            this.db.Albums.AddRange(firstAlbum, secondAlbum, thirdAlbum);

            await this.db.SaveChangesAsync();
        }
    }
}

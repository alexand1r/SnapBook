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

    public class AlbumServiceTests
    {
        public AlbumServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task FindShouldReturnNoResultOnNullSearchText()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var albumService = new AlbumService(db);

            // Act
            var result = await albumService.Find(null);

            // Assert
            result
                .Should()
                .HaveCount(0);
        }

        [Fact]
        public async Task FindShouldReturnCorrectResult()
        {
            // Arrange
            var db = Tests.GetDatabase();

            var firstAlbum = new Album { Id = 1, Title = "First", Description = "test" };
            var secondAlbum = new Album { Id = 2, Title = "Second", Description = "asd" };
            var thirdAlbum = new Album { Id = 3, Title = "Third", Description = "test" };

            db.Albums.AddRange(firstAlbum, secondAlbum, thirdAlbum);

            await db.SaveChangesAsync();
            
            var albumService = new AlbumService(db);

            var searchText = "";

            // Act
            var result = await albumService.Find(searchText);

            // Assert
            result
                .Should()
                .BeOfType<List<AlbumListingServiceModel>>();
        }

        private SnapbookDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<SnapbookDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new SnapbookDbContext(dbOptions);
        }
    }
}

namespace Snapbook.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Snapbook.Data;
    using Snapbook.Data.Models;
    using Snapbook.Services.Implementations;
    using Snapbook.Services.Models.Comment;
    using Snapbook.Services.Models.Photo;
    using Xunit;

    public class PhotoServiceTest
    {
        private readonly SnapbookDbContext db;

        public PhotoServiceTest()
        {
            Tests.Initialize();
            this.db = Tests.GetDatabase();
            this.PopulateDb();
        }

        [Fact]
        public async Task EditShouldReturnFalseWithInvalidId()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Edit(10, "newDescription", "newLocation", "newLatitude", "newLongitude", "");

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task EditShouldReturnCorrectResults()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Edit(1, "newDescription", "newLocation", "newLatitude", "newLongitude", "");
            var secondResult = await photoService.FindForEdit(1);

            // Assert
            result.Should().Be(true);

            secondResult.Should().BeOfType<EditPhotoServiceModel>().Subject.Description.Should().Be("newDescription");
            secondResult.Should().BeOfType<EditPhotoServiceModel>().Subject.Location.Should().Be("newLocation");
            secondResult.Should().BeOfType<EditPhotoServiceModel>().Subject.Latitude.Should().Be("newLatitude");
            secondResult.Should().BeOfType<EditPhotoServiceModel>().Subject.Longitude.Should().Be("newLongitude");

            secondResult.Should().BeOfType<EditPhotoServiceModel>().Subject.Tags.Should().HaveCount(0);
        }

        [Fact]
        public async Task CommentShouldReturnCorrectResults()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Comment(1, "comment", "1");

            // Assert
            result
                .Should()
                .BeOfType<List<CommentServiceModel>>();

            result.Should()
                .Match(r =>
                    r.ElementAt(0).Content == "comment"
                    && r.ElementAt(0).Author.Name == "pesho")
                .And
                .HaveCount(1);
        }

        [Fact]
        public async Task LikeShouldReturnCorrectResultsWithAlreadyLikedPhotoFromThatUser()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Like("1", 1);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task LikeShouldReturnCorrectResultsWithInvalidPhotoId()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Like("2", 10);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task LikeShouldReturnCorrectResults()
        {
            // Arrange
            var photoService = new PhotoService(this.db);

            // Act
            var result = await photoService.Like("2", 1);

            // Assert
            result.Should().Be(2);
        }

        private async void PopulateDb()
        {
            var tag1 = new Tag {Id = 1, Content = "tag1", PhotoId = 1};
            var tag2 = new Tag {Id = 2, Content = "tag2", PhotoId = 2};
            var tag3 = new Tag {Id = 3, Content = "tag3", PhotoId = 3};
            var user1 = new User {Id = "1", UserName = "pesho", Name = "pesho"};
            var user2 = new User {Id = "2", UserName = "gosho", Name = "gosho"};
            var firstAd = new Ad { Id = 1, Name = "Ad", UserId = "1" };
            var firstCategory = new Category { Id = 1, Name = "asd" };
            var firstAlbum = new Album { Id = 1, Title = "Album", CategoryId = 1, UserId = "1" };
            var firstPhoto = new Photo
            {
                Id = 1, ImageUrl = "imageUrl", Description = "First", AdId = 1, Location = "New York", Latitude = "111.1", Longitude = "111.1"
            };
            var secondPhoto = new Photo
            {
                Id = 2, ImageUrl = "imageUrl", Description = "Second", AlbumId = 1, Location = "Washington", Latitude = "222.2", Longitude = "222.2"
            };
            var thirdPhoto = new Photo
            {
                Id = 3, ImageUrl = "imageUrl", Description = "Third", AdId = 1, Location = "Las Vegas", Latitude = "333.3", Longitude = "333.3"
            };

            var userLikedImages = new UsersLikedImages{PhotoId = 1, UserId = "1"};

            this.db.Users.AddRange(user1, user2);
            this.db.Categories.Add(firstCategory);
            this.db.Albums.Add(firstAlbum);
            this.db.Ads.Add(firstAd);
            this.db.Photos.AddRange(firstPhoto, secondPhoto, thirdPhoto);
            this.db.UsersLikedImages.Add(userLikedImages);
            this.db.Tags.AddRange(tag1, tag2, tag3);

            await this.db.SaveChangesAsync();
        }
    }
}

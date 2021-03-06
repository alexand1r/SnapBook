﻿namespace Snapbook.Tests.Services
{
    using FluentAssertions;
    using Snapbook.Data;
    using Snapbook.Data.Models;
    using Snapbook.Services.Advertiser.Implementations;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class AdvertiserAdServiceTest
    {
        private readonly SnapbookDbContext db;

        public AdvertiserAdServiceTest()
        {
            this.db = Tests.GetDatabase();
            this.PopulateDb();
        }

        [Fact]
        public async Task CreateAdShouldReturnFalseWithInvalidUserId()
        {
            // Arrange
            Tests.Initialize();
            var advertiserAdService = new AdvertiserAdService(this.db);

            // Act
            var result = await advertiserAdService.CreateAd("newName", "newDescription", "newUrl", "newWebsite", "10");

            // Assert
            result.Should().Be(false);
        }

        [Fact]
        public async Task CreateAdShouldReturnCorrectResults()
        {
            // Arrange
            Tests.Initialize();
            var advertiserAdService = new AdvertiserAdService(this.db);

            // Act
            var result = await advertiserAdService.CreateAd("newName", "newDescription", "newUrl", "newWebsite", "1");
            var adsCount = this.db.Ads.Count();
            var ad = this.db.Ads.First();

            // Assert
            result.Should().Be(true);
            adsCount.Should().Be(1);
            ad.Name.Should().Be("newName");
            ad.Description.Should().Be("newDescription");
            ad.AdProfilePicUrl.Should().Be("newUrl");
            ad.Website.Should().Be("newWebsite");
            ad.User.Name.Should().Be("pesho");
        }

        private async void PopulateDb()
        {
            var user1 = new User { Id = "1", UserName = "pesho", Name = "pesho" };
           
            this.db.Users.AddRange(user1);

            await this.db.SaveChangesAsync();
        }
    }
}

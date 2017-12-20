namespace Snapbook.Tests.Web.Controllers
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Snapbook.Data.Models;
    using Snapbook.Services;
    using Snapbook.Services.Advertiser.Models;
    using Snapbook.Services.Models.Notification;
    using Snapbook.Services.Models.User;
    using Snapbook.Tests.Mocks;
    using Snapbook.Web.Controllers;
    using Snapbook.Web.Models.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class UsersControllerTest
    {
        private const string FirstUserId = "1";
        private const string FirstUserUsername = "First";

        [Fact]
        public void UsersControllerShouldBeOnlyForAuthorizedUsers()
        {
            // Arrange
            var controller = typeof(UsersController);

            // Act
            var areaAttribute = controller
                    .GetCustomAttributes(true)
                    .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
        }

        [Fact]
        public async Task ChangeProfilePicShouldReturnViewWithValidModel()
        {
            // Arrange
            var userManager = this.UserManagerMockWithSetup();

            var controller = new UsersController(null, null, null, userManager.Object);

            // Act
            var result = await controller.ChangeProfilePic(FirstUserUsername);

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<EditProfilePicViewModel>();
        }
        
        [Fact]
        public async Task ProfileShouldReturnNotFoundWithInvalidUsername()
        {
            // Arrange
            var userManager = UserManagerMock.New;

            var controller = new UsersController(null, null, null, userManager.Object);

            // Act
            var result = await controller.Profile("Username");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
            Assert.Equal("NotFoundPage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ProfileShouldReturnViewWithCorrectModelWithValidUsername()
        {
            // Arrange
            var userManager = this.UserManagerMockWithSetup();

            var userService = this.UserServiceMock();

            var controller = new UsersController(
                userService.Object,
                null,
                null,
                userManager.Object);

            // Act
            var result = await controller.Profile(FirstUserUsername);

            // Assert
            result
                .Should()
                .BeOfType<ViewResult>()
                .Subject
                .Model
                .Should()
                .Match(m => m.As<UserProfileServiceModel>().UserName == FirstUserUsername);
        }

        [Fact]
        public async Task NotificationsShouldReturnNotFoundWithInvalidUsername()
        {
            // Arrange
            var userManager = UserManagerMock.New;

            var controller = new UsersController(null, null, null, userManager.Object);

            // Act
            var result = await controller.Notifications("Username");

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
            Assert.Equal("NotFoundPage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task NotificationsShouldReturnViewWithCorrectModelWithValidUsername()
        {
            // Arrange
            var userManager = this.UserManagerMockWithSetup();

            var notificationService = this.NotificationServiceMock();

            var controller = new UsersController(
                null,
                null,
                notificationService.Object,
                userManager.Object);

            // Act
            var result = await controller.Notifications(FirstUserUsername);

            // Assert
            result.Should().BeOfType<ViewResult>();

            var notificationModel = result.As<ViewResult>().Model.As<List<NotificationServiceModel>>();

            notificationModel.Should().Match(c => c.As<List<NotificationServiceModel>>().Count == 1);
            notificationModel.First().Should().Match(c => c.As<NotificationServiceModel>().PhotoId == 1);
        }

        [Fact]
        public async Task AdsShouldReturnNotFoundWithInvalidId()
        {
            // Arrange
            var adService = this.AdServiceMock();

            var controller = new UsersController(null, adService.Object, null, null);

            // Act
            var result = await controller.Ad(4);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
            Assert.Equal("NotFoundPage", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AdsShouldReturnViewWithCorrectModelWithValidId()
        {
            // Arrange
            var adService = this.AdServiceMock();

            var controller = new UsersController(null, adService.Object, null, null);

            // Act
            var result = await controller.Ad(1);

            // Assert
            result.Should().BeOfType<ViewResult>();

            var adModel = result.As<ViewResult>().Model.As<AdDetailsViewModel>();
            
            adModel.Should().Match(c => c.As<AdDetailsViewModel>().Ad.Name == FirstUserUsername);
        }

        private Mock<IAdService> AdServiceMock()
        {
            var adService = new Mock<IAdService>();
            adService
                .Setup(u => u.FindById(It.Is<int>(id => id == 1)))
                .ReturnsAsync(new AdDetailsServiceModel { Name = FirstUserUsername });

            return adService;
        }

        private Mock<INotificationService> NotificationServiceMock()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService
                .Setup(u => u.All(It.Is<string>(id => id == FirstUserId)))
                .ReturnsAsync(new List<NotificationServiceModel>
                {
                    new NotificationServiceModel { PhotoId = 1 }
                });

            return notificationService;
        }

        private Mock<IUserService> UserServiceMock()
        {
            var userService = new Mock<IUserService>();
            userService
                .Setup(u => u.ProfileAsync(It.Is<string>(id => id == FirstUserId)))
                .ReturnsAsync(new UserProfileServiceModel { UserName = FirstUserUsername });

            return userService;
        }

        private Mock<Microsoft.AspNetCore.Identity.UserManager<User>> UserManagerMockWithSetup()
        {
            var userManager = UserManagerMock.New;
            userManager
                .Setup(u => u.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new User { Id = FirstUserId, UserName = FirstUserUsername });

            return userManager;
        }
    }
}

namespace Snapbook.Tests.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using Snapbook.Services.Admin;
    using Snapbook.Services.Admin.Models.Ad;
    using Snapbook.Web;
    using Snapbook.Web.Areas.Admin.Controllers;
    using Snapbook.Web.Areas.Admin.Models.Ads;
    using Xunit;

    public class AdsControllerTest
    {
        [Fact]
        public void AdsControllerShouldBeInAdminArea()
        {
            // Arrange
            var controller = typeof(AdsController);

            // Act
            var areaAttribute = controller
                    .GetCustomAttributes(true)
                    .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void AdsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(AdsController);

            // Act
            var areaAttribute = controller
                    .GetCustomAttributes(true)
                    .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public async Task AllShouldReturnViewWithCorrectModel()
        {
            // Arrange
            var adService = this.AdServiceMockForAll();

            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.All();

            // Assert
            result.Should().BeOfType<ViewResult>();

            var adModel = result.As<ViewResult>().Model.As<List<AdListingServiceModel>>();

            adModel.Should().Match(c => c.As<List<AdListingServiceModel>>().Count == 1);
            adModel.First().Should().Match(c => c.As<AdListingServiceModel>().Id == 1);
        }

        [Fact]
        public async Task EditShouldReturnNotFoundWithInvalidId()
        {
            // Arrange
            var adService = this.AdServiceMockForEdit();

            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.Edit(4);

            // Assert
            result
                .Should()
                .BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task EditShouldReturnViewWithCorrectModelWithValidId()
        {
            // Arrange
            var adService = this.AdServiceMockForEdit();

            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.Edit(1);

            // Assert
            result.Should().BeOfType<ViewResult>();

            var adModel = result.As<ViewResult>().Model.As<EditAdViewModel>();

            adModel.Should().Match(c => c.As<EditAdViewModel>().Name == "ad");
        }

        [Fact]
        public async Task PostEditShouldReturnBadRequestWhenIdIsInvalid()
        {
            // Arrange
            var adService = this.AdServiceMockForEdit();

            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.Edit(2, new EditAdViewModel());

            // Assert

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task PostEditShouldReturnViewWithCorrectModelWhenModelStateIsInvalid()
        {
            // Arrange
            var adService = this.AdServiceMockForEdit();

            var controller = new AdsController(adService.Object);
            controller.ModelState.AddModelError(string.Empty, "Error");

            // Act
            var result = await controller.Edit(1, new EditAdViewModel());

            // Assert
            result.Should().BeOfType<ViewResult>();

            var model = result.As<ViewResult>().Model;

            model.Should().BeOfType<EditAdViewModel>();
        }

        [Fact]
        public async Task PostEditShouldReturnRedirectWithValidModel()
        {
            // Arrange
            const string nameValue = "Name";
            const string descriptionValue = "Description";
            const string profilePicUrlValue = "ImageUrl";
            const string websiteValue = "www.website.com";
            const int idValue = 1;
            
            string modelName = null;
            string modelDescription = null;
            string modelProfilePicUrl = null;
            string modelWebsite = null;
            int modelId = 0;
            string successMessage = null;

            var adService = new Mock<IAdminAdService>();
            adService
                .Setup(u => u.Edit(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()))
                .Callback((string name, string description, string adProfilePicUrl, string website, int id) =>
                {
                    modelName = name;
                    modelDescription = description;
                    modelProfilePicUrl = adProfilePicUrl;
                    modelWebsite = website;
                    modelId = id;
                }).Returns(Task.FromResult(true));

            var tempData = new Mock<ITempDataDictionary>();
            tempData
                .SetupSet(t => t[WebConstants.TempDataSuccessMessage] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new AdsController(adService.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Edit(idValue, new EditAdViewModel
            {
                Name = nameValue,
                Description = descriptionValue,
                AdProfilePicUrl = profilePicUrlValue,
                Website = websiteValue
            });

            // Assert
            modelName.Should().Be(nameValue);
            modelDescription.Should().Be(descriptionValue);
            modelProfilePicUrl.Should().Be(profilePicUrlValue);
            modelWebsite.Should().Be(websiteValue);
            modelId.Should().Be(idValue);

            successMessage.Should().Be($"Ad {nameValue} details have been successfully changed.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Ad");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Users");
            result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
            result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain(idValue);
        }

        [Fact]
        public async Task PostDeleteShouldReturnNotFoundWithInValidId()
        {
            // Arrange
            const int idValue = 1;

            int modelId = 0;

            var adService = new Mock<IAdminAdService>();
            adService
                .Setup(u => u.Delete(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    modelId = id;
                }).Returns(Task.FromResult(false));

            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.Delete(idValue);

            // Assert
            modelId.Should().Be(idValue);

            result.Should().BeOfType<string>();

            result.Should().Be($"Ad Not Found");
        }

        [Fact]
        public async Task PostDeleteShouldReturnRedirectWithValidId()
        {
            // Arrange
            const int idValue = 1;
            
            int modelId = 0;

            var adService = new Mock<IAdminAdService>();
            adService
                .Setup(u => u.Delete(It.IsAny<int>()))
                .Callback((int id) =>
                {
                    modelId = id;
                }).Returns(Task.FromResult(true));
            
            var controller = new AdsController(adService.Object);

            // Act
            var result = await controller.Delete(idValue);

            // Assert
            modelId.Should().Be(idValue);
            
            result.Should().BeOfType<string>();

            result.Should().Be($"The ad has been succesffuly deleted.");
        }

        private Mock<IAdminAdService> AdServiceMockForEdit()
        {
            var adService = new Mock<IAdminAdService>();
            adService
                .Setup(u => u.FindForEdit(It.Is<int>(id => id == 1)))
                .ReturnsAsync(new AdEditServiceModel { Name = "ad" });

            return adService;
        }

        private Mock<IAdminAdService> AdServiceMockForAll()
        {
            var adService = new Mock<IAdminAdService>();
            adService
                .Setup(u => u.All())
                .ReturnsAsync(new List<AdListingServiceModel>
                {
                    new AdListingServiceModel { Id = 1 }
                });

            return adService;
        }
    }
}

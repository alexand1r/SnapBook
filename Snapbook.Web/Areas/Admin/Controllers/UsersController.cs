namespace Snapbook.Web.Areas.Admin.Controllers
{
    using Data.Models;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.Users;
    using Services.Admin;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersController : BaseController
    {
        private readonly IAdminCategoryService categories;
        private readonly IAdminUserService adminUsers;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        
        public UsersController(
            IAdminCategoryService categories,
            IAdminUserService adminUsers, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<User> userManager)
        {
            this.categories = categories;
            this.adminUsers = adminUsers;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AddToRole()
        {
            var users = await this.adminUsers.AllAsync();

            foreach (var user in users)
            {
                var u = await this.userManager.FindByIdAsync(user.Id);
                var userRoles = await this.userManager.GetRolesAsync(u);
                user.UserRoles = userRoles;
            }

            var roles = await this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToListAsync();

            return this.View(new AdminUserListingViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]  
        public async Task<IActionResult> AddToRole(AddUserToRoleViewModel model)
        {
            var roleExist = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);

            if (!roleExist || user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddToRole));
            }

            this.TempData.AddSuccessMessage($"User {user.UserName} successfully added to {model.Role} role.");
            await this.userManager.AddToRoleAsync(user, model.Role);

            return this.RedirectToAction(nameof(this.AddToRole));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(RemoveRoleViewModel model)
        {
            var roleExist = await this.roleManager.RoleExistsAsync(model.Role);
            var user = await this.userManager.FindByIdAsync(model.UserId);

            if (!roleExist || user == null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid identity details.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.AddToRole));
            }

            this.TempData.AddSuccessMessage($"User {user.UserName} successfully removed from {model.Role} role.");
            await this.userManager.RemoveFromRoleAsync(user, model.Role);

            return this.RedirectToAction(nameof(this.AddToRole));
        }

        public async Task<IActionResult> SeedCategories()
        {
            var cts = new[]
            {
                WebConstants.AbstractCategory,
                WebConstants.AnimalsCategory,
                WebConstants.BusinessCategory,
                WebConstants.ComicsCategory,
                WebConstants.IllustrationsCategory,
                WebConstants.MangaCategory,
                WebConstants.MemesCategory,
                WebConstants.NatureCategory,
                WebConstants.PeopleCategory,
                WebConstants.TechnologyCategory
            };

            foreach (var category in cts)
            {
                var categoryExists = await this.categories.ExistsAsync(category);

                if (!categoryExists)
                {
                    this.categories.Create(category);
                }
            }

            return this.View();
        }
    }
}

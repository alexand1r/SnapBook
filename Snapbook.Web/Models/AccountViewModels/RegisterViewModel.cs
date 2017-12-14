namespace Snapbook.Web.Models.AccountViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class RegisterViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = UserNameMinLength)]
        public string Name { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
        
        [Display(Name = "Is this an advertisement account?")]
        public bool IsAdvertiser { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.BirthDate > DateTime.Now.AddYears(-6))
            {
                yield return new ValidationResult("You have to be atleast 6 years old to register.", new [] {nameof(this.BirthDate)});
            }
        }
    }
}

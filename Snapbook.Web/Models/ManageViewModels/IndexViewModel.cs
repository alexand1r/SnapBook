namespace Snapbook.Web.Models.ManageViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [StringLength(UserNameMaxLength, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = UserNameMinLength)]
        public string Name { get; set; }

        [StringLength(UserBioMaxLength, ErrorMessage = "The {0} must be no more than {1} characters long.")]
        public string Bio { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string StatusMessage { get; set; }
    }
}

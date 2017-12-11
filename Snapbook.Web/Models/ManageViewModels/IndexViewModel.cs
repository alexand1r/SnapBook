namespace Snapbook.Web.Models.ManageViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class IndexViewModel
    {
        public string Username { get; set; }

        [Required]
        [MinLength(UserNameMinLength)]
        [MaxLength(UserNameMaxLength)]
        public string Name { get; set; }

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

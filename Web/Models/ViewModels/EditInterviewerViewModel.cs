using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels
{
    public class EditInterviewerViewModel
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = "First name")]
        [Required]
        [StringLength(254)]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        [StringLength(254)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "New password")]
        [StringLength(10, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Display(Name = "Password confirmation")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string NewPasswordConfirm { get; set; }
    }
}
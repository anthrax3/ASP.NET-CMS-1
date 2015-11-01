using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a username", AllowEmptyStrings = false)]
        [DataType(DataType.Text, ErrorMessage = "E-Mail is invalid")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Enter a password", AllowEmptyStrings = false)]
        [DataType(DataType.Password, ErrorMessage = "Password is invalid")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Enter an email", AllowEmptyStrings = false)]
        [Display(Name = "E-Mail"), DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter your first name", AllowEmptyStrings = false)]
        [Display(Name = "First Name"), DataType(DataType.Text, ErrorMessage = "First name is invalid")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter your last name", AllowEmptyStrings = false)]
        [Display(Name = "Last Name"), DataType(DataType.Text, ErrorMessage = "Last name is invalid")]
        public string LastName { get; set; }
        [StringLength(150), DataType(DataType.Text, ErrorMessage = "About is invalid")]
        public string About { get; set; }
        [Display(Name = "Avatar URL"), DataType(DataType.ImageUrl, ErrorMessage = "Avatar URL is invalid")]
        public string Avatar { get; set; }
        [Required(ErrorMessage = "Enter your location", AllowEmptyStrings = false)]
        [DataType(DataType.Text, ErrorMessage = "Location is invalid")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Enter your phone number", AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone is invalid")]
        public string Phone { get; set; }
        [Display(Name = "Registration Date"), DataType(DataType.DateTime)]
        public DateTime RegisteredOn { get; set; }
        [Display(Name = "Administrator")]
        public int IsAdmin { get; set; }
    }
}
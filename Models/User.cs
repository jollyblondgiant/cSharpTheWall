using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class User
    {
        [Key]
        public int ID {get;set;}
        [Required(ErrorMessage="What's your first name, traveler?")]
        [MinLength(3, ErrorMessage="Ahh, but what's your real name?")]
        [Display(Name="First Name: ")]
        public string FirstName {get;set;}
        [Required(ErrorMessage="What's your last name, traveler?")]
        [MinLength(3, ErrorMessage="Ahh, but what's your real name?")]
        [Display(Name="Last Name: ")]

        public string LastName {get;set;}
        [Required(ErrorMessage="What's your email address?")]
        [EmailAddress(ErrorMessage="Hey! no funny business!")]
        [Display(Name="Email Address: ")]
        public string Email {get;set;}
        [Required(ErrorMessage="What's the secret word?")]
        [MinLength(8, ErrorMessage="YOU MUST CONSTRUCT MORE PYLONS")]
        [DataType(DataType.Password)]
        public string Password {get;set;}
        [Compare("Password", ErrorMessage="Heyy! that's not what you said!")]
        [NotMapped]
        [DataType(DataType.Password)]

        public string ConfirmPassword {get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Post> Posts {get;set;}
        public List<Comment> Comments {get;set;}

    }
}
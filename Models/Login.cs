using System;
using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Login
    {
        [Required(ErrorMessage="Gotta gimme that email, dawg")]
        [Display(Name="Email: ")]
        public string LoginEmail{get;set;}
        [Required(ErrorMessage="What's your safe word, babe?")]
        [Display(Name="Password: ")]
        [DataType(DataType.Password)]
        public string LoginPassword{get;set;}
    }
}
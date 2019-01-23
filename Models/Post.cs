using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class Post
    {
        [Key]
        public int ID {get;set;}
        [Required(ErrorMessage="Speak up, Coward!")]
        [MinLength(8, ErrorMessage="Is that all you have to say?")]
        [Display(Name="Post a message: ")]
        public string PostText{get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public List<Comment> Comments {get;set;}
        public User User {get;set;}
        public int UserID {get;set;}
    }
}
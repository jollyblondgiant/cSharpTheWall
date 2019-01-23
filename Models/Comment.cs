using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TheWall.Models
{
    public class Comment
    {
        [Key]
        public int ID{get;set;}
        [Required(ErrorMessage="Got Something to Add?")]
        [MinLength(3, ErrorMessage="Go on...")]
        [Display(Name="Post a comment: ")]
        public string Text {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        public User User {get;set;}
        public int UserID {get;set;}
        public Post Post {get;set;}
        public int PostID {get;set;}
    }
}
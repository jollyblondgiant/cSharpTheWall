using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using TheWall.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers
{
    public class HomeController : Controller
    {
        private WallContext _dbContext;
        public HomeController(WallContext context)
        {
            _dbContext = context;
        }
        // GET: /Home/
        [HttpGet("")]
        public IActionResult Index()
        {
            List<Post> posts = _dbContext.Posts
            .Include(p=>p.Comments)
                .ThenInclude(c=>c.User).OrderByDescending(p=>p.CreatedAt)
            .Include(p=>p.User)
            .ToList();

            

            ViewBag.Posts = posts;
            ViewBag.seshUser = @HttpContext.Session.GetInt32("ID");
            return View();
        }
        [HttpPost("login")]
        public IActionResult LogIn(Login logUser)
        {
            
            if(_dbContext.Users.Any(u=>u.Email == logUser.LoginEmail) == false)
            {
                ModelState.AddModelError("LoginEmail", "Email Not Found! Please register");
                HttpContext.Session.SetString("SeeReg", "New User? Register!");
                

            }
            if(ModelState.IsValid)
            {
                User _logUser = _dbContext.Users.FirstOrDefault(u=>u.Email == logUser.LoginEmail);
                HttpContext.Session.SetString("UserName", _logUser.FirstName+" "+_logUser.LastName);
                HttpContext.Session.SetInt32("ID", _logUser.ID);
                
                return Redirect("/");
            }
            
            return View("Index");
        }
        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {
            if(_dbContext.Users.Any(u=>u.Email == newUser.Email))
            {
                ModelState.AddModelError("Email", "That Email is already in Use!");
            }
            if(ModelState.IsValid)
            {
                _dbContext.Users.Add(newUser);
                _dbContext.SaveChanges();
                HttpContext.Session.SetInt32("ID", newUser.ID);
                HttpContext.Session.SetString("UserName", newUser.FirstName+" "+newUser.LastName);

                return Redirect("/");
            }
            return View("Index");
        }

        [HttpPost("NewPost")]
        public IActionResult NewPost(Post newPost)
        {
            
            if(ModelState.IsValid)
            {
                int? seshUser = HttpContext.Session.GetInt32("ID");
                newPost.UserID = (int)seshUser;
                newPost.CreatedAt = DateTime.Now;
                _dbContext.Posts.Add(newPost);
                _dbContext.SaveChanges();
                return Redirect("/");
            }
            List<Post> posts = _dbContext.Posts
            .Include(p=>p.Comments)
                .ThenInclude(c=>c.User).OrderByDescending(p=>p.CreatedAt)
            .Include(p=>p.User)
            .ToList();
            ViewBag.Posts = posts;
            return View("Index");
        }
        [HttpGet("logout")]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
        [HttpPost("addComment")]
        public IActionResult AddComment(Comment newComment)
        {
            if(ModelState.IsValid)
            {
                int? seshUser = HttpContext.Session.GetInt32("ID");
                newComment.UserID = (int)seshUser;
                newComment.CreatedAt = DateTime.Now;
                newComment.User = _dbContext.Users.FirstOrDefault(u=>u.ID==seshUser);
                _dbContext.Comments.Add(newComment);
                
                _dbContext.SaveChanges();
                return Redirect("/");
            }
            List<Post> posts = _dbContext.Posts
            .Include(p=>p.Comments)
                .ThenInclude(c=>c.User).OrderByDescending(p=>p.CreatedAt)
            .Include(p=>p.User)
            .ToList();
            ViewBag.Posts = posts;
            return View("Index");
        }
        [HttpGet("deleteComment/{commentID}")]
        public IActionResult DeleteComment(int commentID)
        {
            Comment thisComment = _dbContext.Comments.FirstOrDefault(c=>c.ID == commentID);
            _dbContext.Comments.Remove(thisComment);
            _dbContext.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("deletePost/{postID}")]
        public IActionResult DeletePost(int postID)
        {
            Post thisPost = _dbContext.Posts.FirstOrDefault(c=>c.ID == postID);
            _dbContext.Posts.Remove(thisPost);
            _dbContext.SaveChanges();
            return Redirect("/");
        }
    }
}

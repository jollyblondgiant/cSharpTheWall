using Microsoft.EntityFrameworkCore;
using TheWall.Models;
namespace TheWall.Data
{
    public class WallContext : DbContext
    {
        public WallContext(DbContextOptions<WallContext> options) : base(options){}
        public DbSet<Post> Posts{get;set;}
        public DbSet<User> Users{get;set;}
        public DbSet<Comment> Comments{get;set;}
    }
}
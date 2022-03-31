using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsMedia.Data;

namespace NewsMedia.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NewsMedia.Data.NewsReport> NewsReport { get; set; }
        public DbSet<NewsMedia.Data.CommentItem> CommentItem { get; set; }
    }
}
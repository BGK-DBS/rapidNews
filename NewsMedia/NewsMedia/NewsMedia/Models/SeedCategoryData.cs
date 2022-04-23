using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NewsMedia.Data;
using System;
using System.Linq;

namespace NewsMedia.Models
{
    public class SeedCategoryData
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        { 
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
            // Look for any categories
                if (context.Category.Any())
                {
                    return;    //DB has been seeded
                }
                context.Category.AddRange(
                    new Category { Name = "Education" },
                    new Category { Name = "Health" },
                    new Category { Name = "Sport" },
                    new Category { Name = "Entertainment" },
                    new Category { Name = "Arts & Culture" },
                    new Category { Name = "Current Affairs" },
                    new Category { Name = "Finance" },
                    new Category { Name = "Business" },
                    new Category { Name = "Technology" },
                    new Category { Name = "General News" }
                );
                context.SaveChanges();
            }
        }
    }
}

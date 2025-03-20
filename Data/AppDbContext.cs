using Microsoft.EntityFrameworkCore;
using JwtAuthApi.Models;

namespace JwtAuthApi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options ): base (options) { }

    }
}

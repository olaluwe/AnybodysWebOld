using AnybodysModels;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AnybodysWebDBLibrary
{
    public class AnybodysDbContext : DbContext
    {
        //TODO: Adding DbSets here...
        private static IConfigurationRoot _configuration;

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }


        //constructors
        public AnybodysDbContext()
        {
            //nothing to do here
        }

        public AnybodysDbContext(DbContextOptions options) 
            : base(options)
        {
            //nothing to do here
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();
                var cnstr = _configuration.GetConnectionString("AnybodysDataDbConnection");
                optionsBuilder.UseSqlServer(cnstr);
            }
        }
    }
}

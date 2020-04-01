using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Appweb.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().ToTable("Collection");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Plant>().ToTable("Plant");
            modelBuilder.Entity<Phone>().ToTable("Phone");
            modelBuilder.Entity<Car>().ToTable("Car");
            modelBuilder.Entity<Like>().ToTable("Like");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Tag>().ToTable("Tag");
        }

    }

}
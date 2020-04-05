using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Appweb.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Item> Items { get; set; }

        //public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            
            Database.EnsureCreated();
            
         
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Like>().ToTable("Like");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Item>().ToTable("Item");
        }

    }

}
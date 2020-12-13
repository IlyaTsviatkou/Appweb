using Appweb.Domain.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Appweb.Infrastructure.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        //public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Item> Items { get; set; }
       // public DbSet<UserItem> UserItems { get; set; }

        //public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
                Database.EnsureCreated(); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           /* modelBuilder.Entity<User>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Item>()
                .HasKey(x => x.ItemID);*/

            modelBuilder.Entity<UserItem>()
                .HasKey(x => new { x.UserId, x.ItemId });
            modelBuilder.Entity<UserItem>()
                .HasOne(x => x.User)
                .WithMany(m => m.UserItems)
                .HasForeignKey(x => x.UserId);
            modelBuilder.Entity<UserItem>()
                .HasOne(x => x.Item)
                .WithMany(e => e.UserItems)
                .HasForeignKey(x => x.ItemId);




            /* 
            modelBuilder.Entity<User>().ToTable("User");
             modelBuilder.Entity<Like>().ToTable("Like");
             modelBuilder.Entity<Comment>().ToTable("Comment");
             modelBuilder.Entity<Collection>().ToTable("Collection");
             modelBuilder.Entity<Tag>().ToTable("Tag");
             modelBuilder.Entity<Item>().ToTable("Item");
         */
        }

    }

}
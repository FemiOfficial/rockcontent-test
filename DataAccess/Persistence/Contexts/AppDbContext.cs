using System;
using DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {


        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Likes> Likes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Likes>(f => f.HasKey(e => e.Id));
        //    modelBuilder.Entity<Users>(f => f.HasKey(e => e.Id));
        //    modelBuilder.Entity<Posts>(f => f.HasKey(e => e.Id));
            
        //}

    }
}
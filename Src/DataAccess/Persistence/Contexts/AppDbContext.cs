using System;
using DataAccess.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Likes> Likes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        :base(dbContextOptions)
        {
            
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AppUserStock> AppUserStocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserStock>(x => x.HasKey(p => new { p.AppUserId, p.StockId}));

            builder.Entity<AppUserStock>()
                .HasOne(p => p.AppUser)
                .WithMany(p => p.AppUserStocks)
                .HasForeignKey(p => p.AppUserId);
            
            builder.Entity<AppUserStock>()
                .HasOne(p => p.Stock)
                .WithMany(p => p.AppUserStocks)
                .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name = "user",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
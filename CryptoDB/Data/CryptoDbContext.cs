using CryptoDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoDB.Data
{
    public class CryptoDbContext : IdentityDbContext<AppUser>
    {
        public CryptoDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CryptoBook>().HasKey(x => new { x.BookId, x.CryptoId });
            modelBuilder.Entity<CryptoBook>()
                .HasOne(x => x.Book)
                .WithMany(x => x.CryptoBooks)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<CryptoBook>()
                .HasOne(x => x.CryptoCurrency)
                .WithMany(x => x.CryptoBooks)
                .HasForeignKey(x => x.CryptoId);

            seedRole(modelBuilder, "PaidMember", "create", "update");
            seedRole(modelBuilder, "Admin", "create", "update", "delete");
            seedRole(modelBuilder, "User", "create");

            modelBuilder.Entity<CryptoCurrency>().HasData(new CryptoCurrency
            {
                Id = 99,
                Name = "PeterCrypto",
                Price = "5500",
                DayPrice = "5450",
                HourPrice = "5500",
                WeekPrice = "5000"
            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                Id = 99
            });
            modelBuilder.Entity<CryptoBook>().HasData(new CryptoBook
            {
                BookId = 99,
                CryptoId = 99
            });
        }

        public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CryptoBook> CryptoBooks { get; set; }



        private int id = 1;
        private void seedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };
            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permissions.Select(permission =>
                new IdentityRoleClaim<string>
                {
                    Id = id++,
                    RoleId = role.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission
                }
            );
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}

using Artizanalii.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artizanalii.Data
{
    public class ArtizanaliiContext : DbContext
    {
        public ArtizanaliiContext(DbContextOptions<ArtizanaliiContext> options) :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Producator> Producators { get; set; }
        public DbSet<UserAddress> UsersAddresses { get; set; }
        public DbSet<Produs> Produs { get; set; }
        public DbSet<UserProdus> UserProdus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source=DESKTOP-I7UGUCL;Initial Catalog=DbArtizanalii;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasOne(x => x.User)
                       .WithOne(x => x.UserAddress)
                       .HasForeignKey<UserAddress>(x => x.UserId);
            });

            modelBuilder.Entity<Produs>(entity =>
            {
                entity.HasOne(x => x.Producator)
                .WithMany(x => x.Produs)
                .HasForeignKey(x => x.ProducatorId);
            });

            modelBuilder.Entity<UserProdus>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User)
                .WithMany(x => x.UserProdus)
                .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Produs)
                .WithMany(x => x.UserProdus)
                .HasForeignKey(x => x.ProdusId);
            });
        }

    }
}

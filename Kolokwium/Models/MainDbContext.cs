using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions options) : base(options)
        {
        }

        public MainDbContext()
        {
        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Music_Track> Music_Tracks { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<MusicLabel> MusicLabels { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Musician>(m =>
            {
                m.HasKey(x => x.IdMusician);
                m.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
                m.Property(x => x.LastName).IsRequired().HasMaxLength(50);
                m.Property(x => x.Nickname).HasMaxLength(20);

                m.HasData(
                    new Musician { IdMusician = 1, FirstName = "Jan", LastName = "Kowalski", Nickname = "JK"},
                    new Musician { IdMusician = 2, FirstName ="Jan2", LastName ="Kowalski2"}
                    );
            });

            modelBuilder.Entity<MusicLabel>(m =>
            {
                m.HasKey(x => x.IdMusicLabel);
                m.Property(x => x.Name).IsRequired().HasMaxLength(50);

                m.HasData(
                    new MusicLabel { IdMusicLabel = 1, Name = "MusicLabael1"},
                    new MusicLabel { IdMusicLabel = 2, Name = "MusicLabel2"}
                    );
            });

            modelBuilder.Entity<Album>(a =>
            {
                a.HasKey(x => x.IdAlbum);
                a.Property(x => x.AlbumName).IsRequired().HasMaxLength(30);
                a.Property(x => x.PublishDate).IsRequired();

                a.HasOne(x => x.MusicLabel).WithMany(x => x.Albums).HasForeignKey(x => x.IdMusicAlbum);
            });
        }
    }
}

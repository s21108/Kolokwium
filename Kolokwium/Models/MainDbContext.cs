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
        public DbSet<Music_Tracks> Music_Tracks { get; set; }
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

                a.HasOne(x => x.MusicLabel).WithMany(x => x.Albums).HasForeignKey(x => x.IdMusicLabel);

                a.HasData(
                    new Album { IdAlbum =1 , AlbumName = "Name", PublishDate = DateTime.Parse("2022-01-01"), IdMusicLabel = 1},
                    new Album { IdAlbum = 2, AlbumName = "Name2", PublishDate = DateTime.Parse("2022-01-02"), IdMusicLabel = 2 }
                    );
            });

            modelBuilder.Entity<Track>(t =>
            {
                t.HasKey(x => x.IdTrack);
                t.Property(x => x.TrackName).IsRequired().HasMaxLength(20);
                t.Property(x => x.Duration).IsRequired();

                t.HasOne(x => x.Album).WithMany(x => x.Tracks).HasForeignKey(x => x.IdMusicAlbum);

                t.HasData(
                    new Track { IdTrack = 1, TrackName = "TrackName", Duration = 20, IdMusicAlbum = 1},
                    new Track { IdTrack = 2, TrackName = "TrackNam2", Duration = 30 }
                    );
            });

            modelBuilder.Entity<Music_Tracks>(m =>
            {
                m.HasKey(x => new
                {
                    x.IdMusician, x.IdTrack
                });

                m.HasOne(x => x.Musician).WithMany(x => x.Music_Tracks).HasForeignKey(x => x.IdMusician);
                m.HasOne(x => x.Track).WithMany(x => x.Music_Tracks).HasForeignKey(x => x.IdTrack);

                m.HasData(
                    new Music_Tracks {IdTrack = 1, IdMusician = 2 },
                    new Music_Tracks { IdTrack =2, IdMusician =1 }
                    );
            });
        }
    }
}

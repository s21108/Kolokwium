using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
    public class DbService :IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Album>> GetAlbums(int idAlbum)
        {
            var albums = await _dbContext.Albums
                .Include(x => x.Tracks)
                .Where(x => x.IdAlbum == idAlbum)
                .Select(x => new Album
                {
                    IdAlbum = x.IdAlbum,
                    AlbumName = x.AlbumName,
                    PublishDate = x.PublishDate,
                    IdMusicLabel = x.IdMusicLabel,
                    Tracks = x.Tracks.Select(x => new Track
                    {
                        IdTrack = x.IdTrack,
                        Album = x.Album,
                        Duration = x.Duration,
                        IdMusicAlbum = x.IdMusicAlbum,
                        TrackName = x.TrackName
                    }).OrderByDescending(x => x.Duration).ToList()
                }).ToListAsync();
            return albums; 
        }
    }
}

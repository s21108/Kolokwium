using Kolokwium.Models;
using Kolokwium.Models.DTO;
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

        public async Task<IEnumerable<SomeSortOfAlbum>> GetAlbums(int idAlbum)
        {
            var albums = await _dbContext.Albums
                .Include(x => x.Tracks)
                .Where(x => x.IdAlbum == idAlbum)
                .Select(x => new SomeSortOfAlbum
                {
                    IdAlbum = x.IdAlbum,
                    AlbumName = x.AlbumName,
                    PublishDate = x.PublishDate,
                    IdMusicLabel = x.IdMusicLabel,
                    SomeSortOfTrack = x.Tracks.Select(x => new SomeSortOfTrack
                    {
                        IdTrack = x.IdTrack,
                        Duration = x.Duration,
                        IdMusicAlbum = x.IdMusicAlbum,
                        TrackName = x.TrackName
                    }).OrderByDescending(x => x.Duration).ToList()
                }).ToListAsync();
            return albums; 
        }

        public async Task<int> GetLastIdAlbum()
        {
            var lastAlbum = await _dbContext.Albums.LastOrDefaultAsync();
            return lastAlbum.IdAlbum;
        }

        public async Task<bool> RemoveMusician(int id)
        {
            var musician = new Musician()
            {
                IdMusician = id
            };
            var m = await _dbContext.Musicians.Where(x => x.IdMusician == id).ToListAsync();

            if (m.Count != 0)
            {
                _dbContext.Attach(musician);
                _dbContext.Remove(musician);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        public async Task DeleteMusician(int id)
        {
            var musician = await _dbContext.Musicians.Where(x => x.IdMusician == id).ToListAsync();
            _dbContext.Musicians.Attach(musician.FirstOrDefault());
            _dbContext.Entry(musician.FirstOrDefault()).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
        }

    }
}

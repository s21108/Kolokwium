using Kolokwium.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Album>> GetAlbums(int idAlbum);
    }
}
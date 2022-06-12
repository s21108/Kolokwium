using Kolokwium.Models;
using Kolokwium.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfAlbum>> GetAlbums(int idAlbum);
        Task<int> GetLastIdAlbum();
        Task<bool> RemoveMusician(int id);
        Task DeleteMusician(int id);
    }
}
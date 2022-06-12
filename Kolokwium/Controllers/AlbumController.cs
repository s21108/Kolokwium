using Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IDbService _dbService;
        public AlbumController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        [Route("{idAlbum}")]
        public async Task<IActionResult> GetAlbums(int idAlbum)
        {
            if (idAlbum > await _dbService.GetLastIdAlbum())
                return NotFound($"Nie znaleziono albumu o id {idAlbum}");
            return Ok(await _dbService.GetAlbums(idAlbum));
        }
    }
}

using Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Controllers
{
    [Route("api/musicians")]
    [ApiController]
    public class MusicianController : ControllerBase
    {
        private readonly IDbService _dbService;
        public MusicianController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpDelete]
        [Route("{idMusician}")]
        public async Task<IActionResult> DeleteMusician(int idMusician)
        {
            
        }
    }
}

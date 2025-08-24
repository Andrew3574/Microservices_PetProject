using Microsoft.AspNetCore.Mvc;
using PlatformService.Models;
using PlatformService.Models.DTOs;
using PlatformService.Services;
using PlatformService.Services.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            return Ok(_platformService.GetAll());
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            return Ok(_platformService.GetById(id));
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] PlatformCreateDto platform)
        {
            _platformService.Create(platform);
            return CreatedAtRoute(nameof(GetAll), platform);
        }
    }
}

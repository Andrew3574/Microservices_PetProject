using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;
        private readonly IPlatformService _platformService;
        private readonly ICommandServiceClient _commandServiceClient;

        public PlatformController(
            IPlatformService platformService,
            ICommandServiceClient commandServiceClient,
            IMapper mapper,
            IMessageBusClient messageBusClient)
        {
            _mapper = mapper;
            _messageBusClient = messageBusClient;
            _platformService = platformService;
            _commandServiceClient = commandServiceClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAll()
        {
            return Ok(_platformService.GetAll());
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformReadDto = _platformService.GetById(id);
            var publishPlatform = _mapper.Map<PlatformPublishDto>(platformReadDto);
            publishPlatform.Event = "Platform_Published";
            _messageBusClient.PublishNewPlatform(publishPlatform);
            return Ok(platformReadDto);
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] PlatformCreateDto platform)
        {
            _platformService.Create(platform);
            try
            {
                _commandServiceClient.SendPlatformHttp(_mapper.Map<PlatformReadDto>(platform));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Exception occured while executing http send to commandservice: {ex.Message}" );
            }
            return Ok();
        }
    }
}

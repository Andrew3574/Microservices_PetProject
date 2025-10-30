using CommandService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("/api/c/[controller]")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly GrpcPlatformService _grpcPlatformService;
        
        public CommandController(GrpcPlatformService grpcPlatformService)
        {
            _grpcPlatformService = grpcPlatformService;

        }
        [HttpGet]
        public ActionResult ReturnAllPlatforms()
        {
            return Ok(_grpcPlatformService.ReturnAllPlatforms());
        }

        [HttpPost]
        public ActionResult TestInbound()
        {
            System.Console.WriteLine("--> test inbound");
            return Ok("test inbound for platformservice");
        }

    }

}
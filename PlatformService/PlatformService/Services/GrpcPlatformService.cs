using AutoMapper;
using Grpc.Core;
using PlatformService.Services.Interfaces;

namespace PlatformService.Services
{
    public class GrpcPlatformService : GrpcPlatforms.GrpcPlatformsBase
    {
        private readonly IPlatformService _platformService;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformService platformService, IMapper mapper)
        {
            _platformService = platformService;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(PlatformRequest platformRequest, ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _platformService.GetAll();
            response.Platforms.AddRange(_mapper.Map<IEnumerable<PlatformModel>>(platforms));
            return Task.FromResult(response);
        }
    }
}
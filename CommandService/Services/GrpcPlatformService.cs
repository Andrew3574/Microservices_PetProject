
using AutoMapper;
using Grpc.Net.Client;
using Microsoft.CodeAnalysis;
using PlatformService;

namespace CommandService.Services
{
    public class GrpcPlatformService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IConfiguration configuration, IMapper mapper)
        {
            _config = configuration;
            _mapper = mapper;
        }

        public Task<PlatformResponse> ReturnAllPlatforms()
        {
            System.Console.WriteLine("--> CONNECTING TO GRPC");
            var channel = GrpcChannel.ForAddress(_config.GetSection("GrpcPlatforms").Value);
            var client = new GrpcPlatforms.GrpcPlatformsClient(channel);
            var request = new PlatformRequest();
            try
            {
                return Task.FromResult(client.GetAllPlatforms(request));            
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("--> ERROR OCCURED WHILE EXECUTING GRPC: {0}", ex.Message);
                return null;
            }
        } 
    }
}
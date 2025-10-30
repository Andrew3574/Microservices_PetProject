using PlatformService.Models.DTOs;

namespace PlatformService.Services.Interfaces
{
    public interface ICommandServiceClient
    {
        Task SendPlatformHttp(PlatformReadDto dto);
    }
}
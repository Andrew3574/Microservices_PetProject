
using PlatformService.Models.DTOs;

namespace PlatformService.Services.Interfaces
{
    public interface IMessageBusClient
    {
        Task PublishNewPlatform(PlatformPublishDto dto);
    }
}
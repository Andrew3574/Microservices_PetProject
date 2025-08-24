using PlatformService.Models;
using PlatformService.Models.DTOs;

namespace PlatformService.Services.Interfaces
{
    public interface IPlatformService
    {
        PlatformReadDto GetById(int id);
        IEnumerable<PlatformReadDto> GetAll();
        void Create(PlatformCreateDto platform);
    }
}
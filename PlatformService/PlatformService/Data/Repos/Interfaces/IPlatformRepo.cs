using PlatformService.Models;

namespace PlatformService.Data.Repos.Interfaces
{
    public interface IPlatformRepo
    {
        void SaveChanges();
        void Create(Platform entity);
        Platform? GetPlatformById(int id);
        IEnumerable<Platform> GetAll();
        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}

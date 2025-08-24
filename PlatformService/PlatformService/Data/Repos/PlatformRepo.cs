using PlatformService.Data.Repos.Interfaces;
using PlatformService.Models;

namespace PlatformService.Data.Repos
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;
        public PlatformRepo(AppDbContext context) 
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void Create(Platform entity)
        {
            _context.Add(entity);
            //_context.SaveChanges();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public IEnumerable<Platform> GetAll()
        {
            return _context.Platforms.AsEnumerable();
        }

        public Platform? GetPlatformById(int id)
        {
            return _context.Platforms.FirstOrDefault(p=>p.Id==id);
        }

        public void Rollback()
        {
            _context.Database.RollbackTransaction();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

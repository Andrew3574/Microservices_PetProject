using AutoMapper;
using PlatformService.Data.Repos.Interfaces;
using PlatformService.Models;
using PlatformService.Models.DTOs;
using PlatformService.Services.Interfaces;

namespace PlatformService.Services
{
    public class PlatformManagementService : IPlatformService
    {
        private readonly IPlatformRepo _repository;
        private readonly ILogger<PlatformManagementService> _logger;
        private readonly IMapper _mapper;
        public PlatformManagementService(IPlatformRepo repository,
            ILogger<PlatformManagementService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public PlatformReadDto? GetById(int id)
        {
            return _mapper.Map<PlatformReadDto>(_repository.GetPlatformById(id));
        }

        public IEnumerable<PlatformReadDto> GetAll()
        {
            return _mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAll());
        }

        public void Create(PlatformCreateDto platformDto)
        {
            try
            {
                _repository.BeginTransaction();
                _repository.Create(_mapper.Map<PlatformCreateDto, Platform>(platformDto));

                _repository.SaveChanges();
                _logger.LogInformation("new platform created");
            }
            catch (Exception)
            {
                _repository.Rollback();
                _logger.LogError("rollback initiated");
            }
            finally
            {
                _repository.CommitTransaction();
                _logger.LogWarning("transaction saved");
            }
        }

    }
}

using PlatformService.Models;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepo
    {
        Task<bool> SaveChangesAsync();

        Task<IEnumerable<Platform>> GetAllAsync();
        Task<Platform?> GetByIdAsync(long id);
        bool Create(Platform platform);
    }
}
using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data.Interfaces
{
    class PlatformRepo(AppDbContext context) : IPlatformRepo
    {
        private readonly AppDbContext _context = context;

        public bool Create(Platform platform)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(platform);
                var res = _context.Platforms.Add(platform);
                return true;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Platform>> GetAllAsync()
        {
            try
            {
                return await _context.Platforms.ToListAsync();
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<Platform?> GetByIdAsync(long id)
        {
            try
            {
                return await _context.Platforms.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await _context.SaveChangesAsync() >= 0);
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
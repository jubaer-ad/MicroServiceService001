using CommandService.Models;

namespace CommandService.Data.Interfaces
{
    public interface ICommandRepo
    {
        Task<bool> SaveChangesAsync();

        #region Platform Related Staffs
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task<bool> CreatePlatformAsync(Platform platform);
        Task<bool> PlatformExistsAsync(int PlatformId);
        #endregion

        #region Command Related Staffs
        Task<IEnumerable<Command>> GetCommandsForPlatformAsync(int platformId);
        Task<Command> GetCommandAsync(int platformId, int commandId);
        Task<bool> CreateCommand(int platformId, Command command);
        #endregion
    }
}
using TManagement.Entities;

namespace TManagement.AppServices.Loockups
{
    public interface ILoockupAppService
    {
        Task<List<LoockupDto>> GetLoockupList(LookupType type);
    }
}
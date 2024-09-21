
namespace TManagement.AppServices.Account
{
    public interface IAccountAppService
    {
        Task<AppResult> ChangeStatus(ChangeStatusDto input);
        Task<UserListItemDto> GetById(int id);
        Task<PageResult<UserListItemDto>> GetPagedResult(DataTableFilterInput input);
        Task<AppResult> Register(RegisterDto input);
    }
}

namespace TManagement.AppServices.ETasks
{
    public interface IETasksAppService
    {
        Task<AppResult> CreateOrEdit(CreateOrEditETaskDto input);
        Task<List<TaskUserDto>> GetAllAllowedUsersToAssign();
        Task<PageResult<TaskListItemDto>> GetPagedResult(DataTableFilterInput input);
        Task<CreateOrEditETaskDto> GetTaskToEdit(int id);
        Task<TaskViewDto> GetTaskToView(int id);
    }
}
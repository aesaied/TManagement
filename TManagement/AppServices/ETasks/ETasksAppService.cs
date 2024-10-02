using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using TManagement.AppServices.Account;
using TManagement.AppServices.Attachments;
using TManagement.Entities;
using System.Linq.Dynamic.Core;
namespace TManagement.AppServices.ETasks
{
    public class ETasksAppService(AppDbContext dbContext, IMapper mapper, IAttachmentsAppService attachmentsAppService) : IETasksAppService
    {


        public async Task<AppResult> CreateOrEdit(CreateOrEditETaskDto input)
        {

            var etask = mapper.Map<ETask>(input);


            var oldUsers = new List<int>();


            //edit
            if (input.Id.HasValue)
            {
                var oldTask = await dbContext.Tasks.Include(s=>s.Users).Include(s => s.Attachments).FirstOrDefaultAsync(s => s.Id == input.Id.Value);

                if (oldTask == null)
                {
                    return new AppResult() { Success = false, Errors = ["Task to edit not found"] };
                }

                oldUsers = oldTask.Users.Select(s => s.UserId).ToList();
                mapper.Map(input, oldTask);

                etask = oldTask;

            }
            // create
            else
            {
                etask.TaskDate= DateTime.Now;
                dbContext.Tasks.Add(etask);
            }


            etask.Users ??= [];



            var newUsers = input.Users.Where(u => oldUsers.Contains(u) == false).ToList();
            var userToDelete = oldUsers.Where(u => input.Users.Contains(u) == false).ToList();

            etask.Users.AddRange(newUsers.Select(userId=>new ETaskUsers() { AssignDate= DateTime.Now,  UserId=userId, IsActive=true, Task=etask  } ) );

           var  deletedUsers= etask.Users.Where(s=> userToDelete.Contains(s.UserId)).ToList();

            deletedUsers.ForEach((s) => { s.IsActive = false; });


            //  files 

            if (etask.Attachments == null)
            {
                etask.Attachments = new List<TaskAttachment>();
            }



            input.OldAttachmentIds ??= [];
           
                var deletedAttachment = etask.Attachments.Where(a => input.OldAttachmentIds.Contains(a.AttachmentId) == false).ToList();


                foreach(var attachment in deletedAttachment)
                {
                    etask.Attachments.Remove(attachment);
                }
            
            if (input.Attachments!=null && input.Attachments.Any())
            {
                foreach (var file in input.Attachments)
                {
                    var result = await attachmentsAppService.UploadAttachment(file);


                    TaskAttachment attachment = new TaskAttachment() { AttachmentId = result.Value, Task = etask };

                    etask.Attachments.Add(attachment);
                }

            }




            await dbContext.SaveChangesAsync();



            return AppResult.Ok();

        }

        public async Task<List<TaskUserDto>> GetAllAllowedUsersToAssign()
        {

            var allUsers = await dbContext.Users.ProjectTo<TaskUserDto>(mapper.ConfigurationProvider).ToListAsync();

            return allUsers;
        }

        public async Task<PageResult<TaskListItemDto>> GetPagedResult(DataTableFilterInput input)
        {
            //  1000
            var totalCount = await dbContext.Tasks.CountAsync();

            var tasksQuery = dbContext.Tasks.AsQueryable();

            //  filter data

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                var trimedFilter = input.Filter.Trim();

                // Like '%'
                tasksQuery = tasksQuery.Where(s => s.Title.Contains(trimedFilter) || s.Description.Contains(trimedFilter));

            }

            // 400

            var filteredRecords = await tasksQuery.CountAsync();


            //  
            // set default order
            if (string.IsNullOrWhiteSpace(input.OrderByColumn))
            {
                input.OrderByColumn = nameof(TaskListItemDto.TaskDate);
                input.OrderBy = "desc"; //  desc
            }
            //if (input.OrderByColumn == nameof(UserListItemDto.FullName))
            //{
            //    if (input.OrderBy == "desc")
            //    {
            //        userQuery= userQuery.OrderByDescending(s => s.FullName);

            //    }
            //    else
            //    {

            //    }

            //}
            var orderedData = tasksQuery.OrderBy($"{input.OrderByColumn} {input.OrderBy}").Skip(input.Start).Take(input.PageSize);

            var data = await orderedData.ProjectTo<TaskListItemDto>(mapper.ConfigurationProvider).ToListAsync();
            return new PageResult<TaskListItemDto> { RecordsTotal = totalCount, RecordsFiltered = filteredRecords, Data = data };
        
    }

        public async Task<CreateOrEditETaskDto> GetTaskToEdit(int id)
        {
            var task = await dbContext.Tasks.Include(s => s.Users).Include(s => s.Attachments).ThenInclude(s=>s.Attachments).FirstOrDefaultAsync(s => s.Id == id);

            if (task == null)
            {

                return null;

            }


            var  output = mapper.Map<CreateOrEditETaskDto>(task);

            return output;
        }

        public async Task<TaskViewDto> GetTaskToView(int id)
        {
            var task = await dbContext.Tasks.ProjectTo<TaskViewDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(s => s.Id == id);

           

            return task;
        }
    }
}

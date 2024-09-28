using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TManagement.Entities;
using TManagement.Services;
using System.Linq.Dynamic.Core;
using AutoMapper.QueryableExtensions;
using Microsoft.Data.SqlClient.DataClassification;
using TManagement.Enums;
namespace TManagement.AppServices.Account
{
    public class AccountAppService(AppDbContext dbContext, IMapper _mapper, INotificationManager notificationManager) : IAccountAppService
    {

        public async Task<AppResult> Register(RegisterDto input)
        {
            //  registerDto ->  AppUser

            AppUser appUser = _mapper.Map<AppUser>(input);

            //  

            if (await dbContext.Users.AnyAsync(s => s.Email == input.Email))
            {
                return new AppResult { Success = false, Errors = ["Email already used by another user"] };
            }

            else
            {
                try
                {
                    PasswordHasher passwordHasher = new PasswordHasher();
                    var hasgedPassword = passwordHasher.HashPassword(input.Password, out var salt);

                    appUser.PasswordHash = hasgedPassword;
                    appUser.PasswordSalt = salt;

                    dbContext.Users.Add(appUser);
                    await dbContext.SaveChangesAsync();

                    return AppResult.Ok();

                }
                catch (Exception ex)
                {

                    return new AppResult { Success = false, Errors = ["Unable to serve your request, Please try again"] };

                }

            }
        }



        public async Task<PageResult<UserListItemDto>> GetPagedResult(DataTableFilterInput input)
        {
            //  1000
            var totalCount = await dbContext.Users.CountAsync();

            var userQuery = dbContext.Users.AsQueryable();

            //  filter data

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                var trimedFilter = input.Filter.Trim();

                // Like '%'
                userQuery = userQuery.Where(s => s.FullName.Contains(trimedFilter) || s.Email.Contains(trimedFilter));

            }

            // 400

            var filteredRecords = await userQuery.CountAsync();


            //  
            // set default order
            if (string.IsNullOrWhiteSpace(input.OrderByColumn))
            {
                input.OrderByColumn = nameof(UserListItemDto.FullName);
                input.OrderBy = "asc"; //  desc
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
            var orderedData = userQuery.OrderBy($"{input.OrderByColumn} {input.OrderBy}").Skip(input.Start).Take(input.PageSize);

            var data = await orderedData.ProjectTo<UserListItemDto>(_mapper.ConfigurationProvider).ToListAsync();
            return new PageResult<UserListItemDto> { RecordsTotal = totalCount, RecordsFiltered = filteredRecords, Data = data };
        }


        public async Task<UserListItemDto> GetById(int id)
        {
            var user = await dbContext.Users.ProjectTo<UserListItemDto>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(x => x.Id == id);

            return user; //_mapper.Map<UserListItemDto>(user);
        }

        public async Task<AppResult> ChangeStatus(ChangeStatusDto input)
        {

            var user = await dbContext.Users.FindAsync(input.Id);
            if (user != null)
            {
                user.CurrentStatus = input.Status;
                await dbContext.SaveChangesAsync();

               await notificationManager.Notify(SysGroups.Admins, new NotificationInfo() { Message = $"User {user.FullName} status changed to {input.Status.ToString()} " });

                return AppResult.Ok();
            }

            return new AppResult() { Success = false, Errors = ["Unable to change user status"] };
        }
    }

}

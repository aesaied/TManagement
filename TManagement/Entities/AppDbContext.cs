using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using TManagement.Enums;
using TManagement.Services;

namespace TManagement.Entities
{
    public class AppDbContext :DbContext
    {

        //  constructor to  enable configure provider and conn str
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }


        public DbSet<AppUser> Users { get; set; }   
        public DbSet<AppGroup> Groups { get; set; }

        public DbSet<AppUserGroup> UserGroups { get; set; }

        public DbSet<Lookup> Lookups { get; set; }

        public DbSet<SystemNotification> SystemNotifications { get; set; }


        public DbSet<ETask> Tasks { get; set; }

        public DbSet<Attachment> Attachments { get; set; }

        public DbSet<TaskAttachment> TaskAttachments { get; set; }

        public DbSet<ETaskUsers> ETaskUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //
            modelBuilder.Entity<ETask>().Property(s => s.CurrentStatus).HasDefaultValueSql("0");
           
            modelBuilder.Entity<AppUser>().HasOne(s=>s.City).WithMany(c=>c.CityUsers).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AppUser>().HasOne(s => s.EducationLevel).WithMany(c=>c.EducationLevelUsers).OnDelete(DeleteBehavior.NoAction);


            Guid palId = Guid.Parse("{CFE43CB8-7B8D-4955-BCA1-491971508A75}");
            Guid jodId = Guid.Parse("{CFE43CB8-7B8D-4955-BCA1-491971508A76}");
            Guid jerusId = Guid.Parse("{CFE43CB8-7B8D-4955-BCA1-491971508A77}");
            Guid baId = Guid.Parse("{CFE43CB8-7B8D-4955-BCA1-491971508A79}");


            //{45EBC68D-99F5-4DBD-B5F5-B205A63F86E4}
            modelBuilder.Entity<Lookup>().HasData(
                
                new Lookup() { Name ="Palestine", Id=palId, Type= LookupType.Country },
                new Lookup() { Name = "Jordan", Id = jodId, Type = LookupType.Country },
                new Lookup() { Name = "Jerusalem", Id = jerusId, Type = LookupType.City, FatherLookupId=palId },
                new Lookup() { Name = "Amman", Id = Guid.Parse("{46BAABEB-030B-49F1-B6E5-CD02E313956E}"), Type = LookupType.City, FatherLookupId=jodId },
                new Lookup() { Name = "Ramallah", Id = Guid.Parse("{8BF52A09-678C-470E-A8E9-AD95FE2C1D7F}"), Type = LookupType.City, FatherLookupId=palId },
                new Lookup() { Name = "Elemantary", Id = Guid.Parse("{E17C10FD-E4C0-47CC-A3BC-C5EF3E229B4A}"), Type = LookupType.EducationLevel },
                new Lookup() { Name = "Tawjihi", Id = Guid.Parse("{29BE72B1-3A33-4188-9FC7-0579ADAC9FFA}"), Type = LookupType.EducationLevel },
                new Lookup() { Name = "BA/BS", Id =baId, Type = LookupType.EducationLevel },
                new Lookup() { Name = "Master and above", Id = Guid.Parse("{F3E1BA09-F161-4F62-824A-8DB683276850}"), Type = LookupType.EducationLevel }






                );


            

            modelBuilder.Entity<AppGroup>().HasData(
                  new AppGroup() { Name = SysGroups.Admins, Id=1},
                  new AppGroup() { Name = SysGroups.Secretary, Id = 2 },
                  new AppGroup() { Name = SysGroups.Reports, Id = 3 }

                );

            var defaultPassword = "123@qwe";

            PasswordHasher hasher = new PasswordHasher();
           var  passwordHash= hasher.HashPassword(defaultPassword, out var salt);


            modelBuilder.Entity<AppUser>().HasData(
                new AppUser() { Id=1, Email="Atallah.esaied@gmail.com", FullName="System admin", CityId=jerusId, EducationLevelId=baId, CurrentStatus= UserStatus.Active, PasswordHash=passwordHash, PasswordSalt=salt }
                
                );


            modelBuilder.Entity<AppUserGroup>().HasData(
                new AppUserGroup() { GroupId=1, UserId=1, Id= Guid.Parse("A7352666-5593-4CFF-9443-6DB0A1B9DCBC") });



            //  composite key
            modelBuilder.Entity<TaskAttachment>().HasKey(s => new { s.TaskId, s.AttachmentId });
            // user  Hashing password

        }
    }
}

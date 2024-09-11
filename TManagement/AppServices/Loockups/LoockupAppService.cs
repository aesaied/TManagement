using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Caching.Memory;
using TManagement.Entities;

namespace TManagement.AppServices.Loockups
{
    public class LoockupAppService(AppDbContext dbContext, IMemoryCache memoryCache, IMapper mapper) : ILoockupAppService
    {

        private const string CACHE_KEY = "Loockups_{0}";
        public async Task<List<LoockupDto>> GetLoockupList(LookupType type)
        {
            string key = string.Format(CACHE_KEY, type);

            if (!memoryCache.TryGetValue<List<LoockupDto>>(key, out var value))
            {
                var data = await dbContext.Lookups.Where(s => s.Type == type).ProjectTo<LoockupDto>(mapper.ConfigurationProvider).ToListAsync();
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromDays(30),
                    Priority = CacheItemPriority.High

                };

                memoryCache.Set(key, data, cacheOptions);


                return data;

            }


            return value;

        }
    }
}

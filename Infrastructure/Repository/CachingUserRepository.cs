using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class CachingUserRepository : UserRepository
    {
        public static TimeSpan UserCachePersistenceTime= TimeSpan.FromMinutes(60);

        private readonly IUserRepository _userRepository;
        private readonly IMemoryCache _memoryCache;

        public CachingUserRepository(ApplicationDbContext applicationDbContext,
                                     IUserRepository userRepository,
                                     IMemoryCache memoryCache) : base(applicationDbContext)
        {
            this._userRepository = userRepository;
            this._memoryCache = memoryCache;
        }

        public async override Task<Domain.Entities.User.User> GetAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var id = expression.ExtractValueFromExpression<DatabaseEntity.User,Guid?>();
            if(id != null)
            {
                var result = await _memoryCache.GetOrCreateAsync(CacheKeys.UserKey(id ?? Guid.Empty), async (cacheEntry) => 
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = UserCachePersistenceTime;
                    return await _userRepository.GetAsync(expression);
                });

                return result;
            }
            return await _userRepository.GetAsync(expression);
        }

        public async override Task<ICollection<Domain.Entities.User.User>> ListAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var result = await _memoryCache.GetOrCreateAsync(CacheKeys.UsersKey("all"), async (cacheEntry) =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = UserCachePersistenceTime;
                return await _userRepository.ListAsync(expression);
            });

            return result;
        }
    }
}

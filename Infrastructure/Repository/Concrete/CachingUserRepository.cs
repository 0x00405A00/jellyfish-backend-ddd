using Domain.Entities.Users;
using Infrastructure.Abstractions;
using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Concrete
{
    internal class CachingUserRepository : UserRepository
    {
        public static TimeSpan UserCachePersistenceTime = TimeSpan.FromMinutes(60);

        private readonly IUserRepository _userRepository;
        private readonly ICachingHandler _memoryCache;

        public CachingUserRepository(ApplicationDbContext applicationDbContext,
                                     IUserRepository userRepository,
                                     ICachingHandler memoryCache) : base(applicationDbContext)
        {
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public async override Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var id = expression.ExtractValueFromExpression<User, Guid?>();
            if (id != null)
            {
                var resultFromCache = await _memoryCache.GetOrSet<User>(CacheKeys.UserKey(id ?? Guid.Empty), async () =>
                {
                    return await _userRepository.GetAsync(expression);
                });

                return resultFromCache;
            }
            var resultFromDatabase = await _userRepository.GetAsync(expression);

            return resultFromDatabase;
        }

        public async override Task<ICollection<User>> ListAsync(Expression<Func<User, bool>> expression)
        {
            /*var result = await _memoryCache.GetOrCreateAsync(CacheKeys.UsersKey("all"), async (cacheEntry) =>
            {
                cacheEntry.AbsoluteExpirationRelativeToNow = UserCachePersistenceTime;
            });*/

            return await _userRepository.ListAsync(expression);
            //return result;
        }
    }
}

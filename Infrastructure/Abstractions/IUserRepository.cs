using Domain.Entities.Users;
using Infrastructure.Repository;
using Shared.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Check if user email is already in database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> IsEmailAlreadyInUse(string email);
        /// <summary>
        /// Fully register state from user
        /// State is fully when user complete the activation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<bool> IsUserRegistered(string email);
        /// <summary>
        /// Check if given phonenumber is already in use by other user
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public Task<bool> IsPhoneAlreadyInUse(string phone);

        /// <summary>
        /// Get requested and received friendship requests
        /// </summary>
        /// <returns></returns>
        public Task<List<FriendshipRequest>> GetAllFriendshipRequests(Expression<Func<User, bool>> expression);
        public Task<ICollection<User>> TestA();
    }
}

using Domain.Entities.Users;
using System.Linq.Expressions;

namespace Domain.Repository
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
        public Task<User> GetWithTrackingWithoutDependenciesAsync(Expression<Func<User, bool>> expression);
    }
}

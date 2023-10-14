using Infrastructure.DatabaseEntity;
using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IUserRepository : IGenericRepository<Domain.Entities.User.User,User>
    {
        /// <summary>
        /// Check is user email is already in database
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
    }
}

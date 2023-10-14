using Infrastructure.Abstractions;

namespace Infrastructure.Repository
{
    internal class AuthRepository : GenericRepository<Infrastructure.DatabaseEntity.Auth>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {   
        
        }
    }
}

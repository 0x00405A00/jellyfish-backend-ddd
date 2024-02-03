using Domain.Entities.Users;
using Domain.Specification;
using System.Linq.Expressions;

namespace Domain.Entities.Users.Specification;

public class UserIsRegisteredSpecification : Specification<User>
{
    private readonly User _user;

    public UserIsRegisteredSpecification(User user)
    {
        _user = user;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return _user => _user.IsFullyRegistered();
    }

    //applicable in repositories
}



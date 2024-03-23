namespace Infrastructure.Abstractions
{
    public interface IMailoutboxRepository : Domain.Repository.IMailoutboxRepository
    {

    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}
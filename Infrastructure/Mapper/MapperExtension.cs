using Infrastructure.Abstractions;

namespace Infrastructure.Mapper
{
    public static class MapperExtension
    {
        static MapperExtension()
        {
            
        }

        public static AbstractMapper<TDomainEntity, TDatabaseEntity> GetMapper<TDomainEntity, TDatabaseEntity>()
            where TDomainEntity : class
            where TDatabaseEntity : DatabaseEntityModel
        {
            var type = typeof(AbstractMapper<TDomainEntity, TDatabaseEntity>);
            var mapperType = AssemblyReference.Assembly.GetTypes().Where(x => x.BaseType == type).FirstOrDefault();
            if (mapperType == null)
            {
                throw new NotImplementedException($"Mapper not found in Assembly {AssemblyReference.Assembly.FullName}");
            }

            return (AbstractMapper<TDomainEntity, TDatabaseEntity>)Activator.CreateInstance(mapperType)!;
        }
        public static async Task<TDomainEntity> MapToDomainEntity<TDomainEntity, TDatabaseEntity>(this TDatabaseEntity databaseEntity, bool withRelation)
            where TDomainEntity : class
            where TDatabaseEntity : DatabaseEntityModel
        {

            var mapper = GetMapper<TDomainEntity, TDatabaseEntity>();
            return await mapper.MapToDomainEntity(databaseEntity, withRelation);//<---bei user update kommt hier eine stackoverflow exception, loop
        }
        public static async Task<TDatabaseEntity> MapToDatabaseEntity<TDomainEntity, TDatabaseEntity>(this TDomainEntity domainEntity, bool mapRelationObjects)
            where TDomainEntity : class
            where TDatabaseEntity : DatabaseEntityModel
        {
            var mapper = GetMapper<TDomainEntity, TDatabaseEntity>();
            return await mapper.MapToDatabaseEntity(domainEntity, mapRelationObjects);
        }
        public static async Task<ICollection<TDomainEntity>> MapToDomainEntity<TDomainEntity, TDatabaseEntity>(this ICollection<TDatabaseEntity> databaseEntity, bool withRelation)
            where TDomainEntity : class
            where TDatabaseEntity : DatabaseEntityModel
        {
            var mapper = GetMapper<TDomainEntity, TDatabaseEntity>();
            return await mapper.MapToDomainEntity(databaseEntity, withRelation);
        }
        public static async Task<ICollection<TDatabaseEntity>> MapToDatabaseEntity<TDomainEntity, TDatabaseEntity>(this ICollection<TDomainEntity> domainEntity, bool mapRelationObjects)
            where TDomainEntity : class
            where TDatabaseEntity : DatabaseEntityModel
        {
            var mapper = GetMapper<TDomainEntity, TDatabaseEntity>();
            return await mapper.MapToDatabaseEntity(domainEntity,mapRelationObjects);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public abstract class AbstractMapper<TDomainEntity,TDatabaseEntity>
        where TDomainEntity : class
        where TDatabaseEntity : DatabaseEntityModel
    {
        public abstract Task<TDomainEntity> MapToDomainEntity(TDatabaseEntity entity, bool withRelation);
        public abstract Task<TDatabaseEntity> MapToDatabaseEntity(TDomainEntity entity, bool mapRelationObjects);
        public async virtual Task<ICollection<TDomainEntity>> MapToDomainEntity(ICollection<TDatabaseEntity> entity, bool withRelation)
        {
            if (entity == null)
                return null!;

            ICollection<TDomainEntity> resp = new List<TDomainEntity>();    
            foreach(var item in entity.ToList()) {

                var domainEntity = await MapToDomainEntity(item, withRelation);
                resp.Add(domainEntity);
            }
            return resp;
        }
        public async virtual Task<ICollection<TDatabaseEntity>> MapToDatabaseEntity(ICollection<TDomainEntity> entity,bool mapRelationObjects)
        {
            if (entity == null)
                return null!;

            ICollection<TDatabaseEntity> resp = new List<TDatabaseEntity>();
            foreach (var item in entity.ToList())
            {

                var dbEntity = await MapToDatabaseEntity(item,mapRelationObjects);
                resp.Add(dbEntity);
            }
            return resp;
        }
    }
}

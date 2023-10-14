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
        public abstract TDomainEntity MapToDomainEntity(TDatabaseEntity entity, bool withRelation);
        public abstract TDatabaseEntity MapToDatabaseEntity(TDomainEntity entity, bool mapRelationObjects);
        public virtual ICollection<TDomainEntity> MapToDomainEntity(ICollection<TDatabaseEntity> entity, bool withRelation)
        {
            if (entity == null)
                return null!;

            ICollection<TDomainEntity> resp = new List<TDomainEntity>();    
            foreach(var item in entity.ToList()) {

                var domainEntity = MapToDomainEntity(item, withRelation);
                resp.Add(domainEntity);
            }
            return resp;
        }
        public virtual ICollection<TDatabaseEntity> MapToDatabaseEntity(ICollection<TDomainEntity> entity,bool mapRelationObjects)
        {
            if (entity == null)
                return null!;

            ICollection<TDatabaseEntity> resp = new List<TDatabaseEntity>();
            foreach (var item in entity.ToList())
            {

                var dbEntity = MapToDatabaseEntity(item,mapRelationObjects);
                resp.Add(dbEntity);
            }
            return resp;
        }
    }
}

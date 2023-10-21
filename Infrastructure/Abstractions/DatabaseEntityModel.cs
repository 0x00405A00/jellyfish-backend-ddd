using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Abstractions
{
    public abstract class DatabaseEntityModel
    {
        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public DatabaseEntityModel()
        {
        }

        
    }
}

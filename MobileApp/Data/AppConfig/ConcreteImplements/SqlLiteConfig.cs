using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Attribute;
using MobileApp.Data.AppConfig.Abstraction;

namespace MobileApp.Data.AppConfig.ConcreteImplements
{
    public class SqlLiteConfig : AbstractApplicationConfig
    {

        public bool SchemaCreated { get; set; }
        public string DatabasePath { get; set; }
    }
}

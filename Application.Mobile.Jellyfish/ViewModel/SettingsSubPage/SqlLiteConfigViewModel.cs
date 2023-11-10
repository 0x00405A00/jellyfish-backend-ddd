using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Attribute;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.ViewModel.SettingsSubPage;

namespace MobileApp.Data.AppConfig.ConcreteImplements
{
    public class SqlLiteConfigViewModel : AbstractConfigViewModel<SqlLiteConfig>
    {
        public SqlLiteConfigViewModel(SqlLiteConfig config) : base(config)
        {
        }

        public bool SchemaCreated { get; set; }

        public override void AddValidations()
        {

        }

        public override void MapConfigDataWithDisplayData()
        {
        }

        public override void Safe()
        {


        }
    }
}
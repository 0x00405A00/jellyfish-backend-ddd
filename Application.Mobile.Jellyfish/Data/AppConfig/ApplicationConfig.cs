using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Data.AppConfig.ConcreteImplements;

namespace MobileApp.Data.AppConfig
{
    public class ApplicationConfig : AbstractApplicationConfig
    {
        public ChatConfig ChatConfig { get; set; }
        public NetworkConfig NetworkConfig { get; set; }
        public AccountConfig AccountConfig { get; set; }
        public NotificationConfig NotificationConfig { get; set; }  
        public SqlLiteConfig SqlLiteConfig { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
            ChatConfig = new ChatConfig();
            NetworkConfig = new NetworkConfig();    
            NotificationConfig = new NotificationConfig();
            SqlLiteConfig = new SqlLiteConfig();
            AccountConfig = new AccountConfig();
        }
    }
}

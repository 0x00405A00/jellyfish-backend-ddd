using MobileApp.ApplicationSpecific;
using MobileApp.Data.AppConfig;

namespace MobileApp.Handler.AppConfig
{
    public static class ApplicationConfigHandlerSingleton
    {
        public static ApplicationConfigHandler Handler;
        static ApplicationConfigHandlerSingleton()
        {

        }
        public static ApplicationConfigHandler Get()
        {
            if(Handler == null)
            {
                Handler = new ApplicationConfigHandler(Global.ApplicationConfigPath, true);
            }
            return Handler;
        }
    }
    public class ApplicationConfigHandler
    {

        public string ConfigPath { get; private set; }
        public bool SafeChangesImmediately { get; private set; }
        public ApplicationConfig ApplicationConfig { get; set; }
        public ApplicationConfigHandler(string configPath,bool safeChangesImmediately)
        {
            ConfigPath = configPath;
            SafeChangesImmediately = safeChangesImmediately;
        }


        public ApplicationConfig Load()
        {
            ApplicationConfig applicationConfig = null;
            try
            {
                if(!File.Exists(ConfigPath))
                {
                    File.Create(ConfigPath);    
                }
                string jsonContentFromFile = File.ReadAllText(ConfigPath);
                if(jsonContentFromFile != null && !String.IsNullOrEmpty(jsonContentFromFile))
                {
                    using(JsonHandler json = new JsonHandler())
                    {
                        applicationConfig = json.JsonDeserialize<ApplicationConfig>(jsonContentFromFile);
                    }
                }
                else
                {
                    applicationConfig = new ApplicationConfig();
                    Safe(applicationConfig);
                }
            }
            catch(Exception ex)
            {

            }
            return applicationConfig;
        }
        public bool Safe(ApplicationConfig applicationConfig = null)
        {
            try
            {
                using (JsonHandler json = new JsonHandler())
                {
                    string jsonStr = json.JsonSerialize<ApplicationConfig>(applicationConfig == null? ApplicationConfig:applicationConfig);
                    File.WriteAllText(ConfigPath, jsonStr);
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

    }
}

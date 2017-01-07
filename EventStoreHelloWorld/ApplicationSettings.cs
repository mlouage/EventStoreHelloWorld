using System;
using System.Configuration;

namespace EventStoreHelloWorld
{
    public class ApplicationSettings : IApplicationSettings
    {
        public ApplicationSettings()
        {
            GesIpAddress = FromAppSetting("GesIpAddress");
            GesHttpPort = TryGetIntAppSetting("GesHttpPort");
            GesTcpIpPort = TryGetIntAppSetting("GesTcpIpPort");
            GesUserName = FromAppSetting("GesUserName");
            GesPassword = FromAppSetting("GesPassword");
        }

        public string GesIpAddress { get; private set; }
        public int GesHttpPort { get; private set; }
        public int GesTcpIpPort { get; private set; }
        public string GesUserName { get; private set; }
        public string GesPassword { get; private set; }

        protected static string FromConnectionStrings(string name)
        {
            var setting = ConfigurationManager.ConnectionStrings[name];
            return setting == null ? string.Empty : setting.ConnectionString;
        }

        protected static string FromAppSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        protected static int TryGetIntAppSetting(string key, int defaultValue = 0)
        {
            try { return Convert.ToInt32(ConfigurationManager.AppSettings[key]); }
            catch (Exception) { return defaultValue; }
        }
    }
}

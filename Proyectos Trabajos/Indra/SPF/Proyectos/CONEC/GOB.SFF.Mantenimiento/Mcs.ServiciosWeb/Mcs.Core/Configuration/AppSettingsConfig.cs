using System.Configuration;

namespace Mcs.Core.Configuration
{
    public static class AppSettingsConfig
    {
        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(key)) return "";

            var result = ConfigurationManager.AppSettings[key];
            return result;
        }
    }
}

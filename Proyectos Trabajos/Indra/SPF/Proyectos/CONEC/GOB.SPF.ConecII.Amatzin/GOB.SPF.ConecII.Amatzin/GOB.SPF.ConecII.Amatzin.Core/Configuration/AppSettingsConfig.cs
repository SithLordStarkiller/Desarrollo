using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using GOB.SPF.ConecII.Amatzin.Core.Resources;

namespace GOB.SPF.ConecII.Amatzin.Core.Configuration
{
    public static class AppSettingsConfig
    {
        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetValueDb()
        {
            return ConfigurationManager.AppSettings[ResourceConfiguration.DbStore];
        }

        public static string GetValueDirectoryStorage()
        {
            return ConfigurationManager.AppSettings[ResourceConfiguration.Path];
        }
    }
}

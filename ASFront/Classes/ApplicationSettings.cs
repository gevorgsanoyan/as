using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ASFront.Classes
{
    public static class ApplicationSettings
    {


        private static int _PageSize = 0;
        private static readonly int _PageSizeDef = 25;



        static public int PageSize
        {


            get
            {


                string _ConfigSettingStr = string.Empty;


                if (_PageSize == 0)
                {

                    if (ConfigurationManager.AppSettings["PageSize"] != null && !string.IsNullOrEmpty(ConfigurationManager.AppSettings["PageSize"]))

                    {
                        _ConfigSettingStr = ConfigurationManager.AppSettings["PageSize"];

                        if (!Int32.TryParse(_ConfigSettingStr, out _PageSize))
                            _PageSize = _PageSizeDef;

                        AddUpdateAppSettings("PageSize", _PageSize.ToString());

                    }
                    else
                    {
                        _PageSize = _PageSizeDef;

                        AddUpdateAppSettings("PageSize", _PageSize.ToString());
                    }

                }
                return _PageSize;
            }


            set
            {
                _PageSize = value;

                AddUpdateAppSettings("PageSize", _PageSize.ToString());
            }
        }


        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                ;
            }
        }




    }
}
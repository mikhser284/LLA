using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LLA.GUI.Dialogs.Settings
{
    public class SettingsBase
    {
        const String EnvVar_L2BaseSettingsFile = "L2_BaseSettingsFilePath";
        private static String _baseSettingsFilePath;

        public static String BaseSettingsFilePath
        {
            get
            {
                if (!String.IsNullOrEmpty(_baseSettingsFilePath)) return _baseSettingsFilePath;
                //
                _baseSettingsFilePath = Environment.GetEnvironmentVariable(EnvVar_L2BaseSettingsFile);
                if (!String.IsNullOrEmpty(_baseSettingsFilePath)) return _baseSettingsFilePath;
                //
                _baseSettingsFilePath = Path.Combine(Environment.CurrentDirectory, "L2_BaseSettings.L2Config");
                Environment.SetEnvironmentVariable(EnvVar_L2BaseSettingsFile, _baseSettingsFilePath);
                //
                return _baseSettingsFilePath;
            }

            set
            {
                if (!File.Exists(value)) throw new FileNotFoundException(value);
                _baseSettingsFilePath = value;
                Environment.SetEnvironmentVariable(EnvVar_L2BaseSettingsFile, _baseSettingsFilePath);
            }
        }

    }
}

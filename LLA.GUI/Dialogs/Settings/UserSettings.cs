using System;
using System.Collections.Generic;
using System.Text;

namespace LLA.GUI.Dialogs.Settings
{
    public class UserSettings
    {
        public Guid SettingsUid { get; set; }

        public String SettingsName { get; set; }

        public String Description { get; set; }

        public String MyDictionaryDefaultFilePath { get; set; }
    }
}

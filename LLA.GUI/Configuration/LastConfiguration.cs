using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LLA.GUI.Configuration
{
    [JsonObject("LastConfiguration")]
    public class LastConfiguration
    {
        [JsonProperty("LastSessionFiles", Order = 1)]
        public HashSet<String> LastSessionFiles { get; set; }

        public LastConfiguration()
        {
            LastSessionFiles = new HashSet<string>();
        }
    }
}

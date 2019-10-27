using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LLA.GUI
{

    public static class UserSession
    {
        public static String SessionFilePath { get; set; }

        public static SessionData Data { get; set; }

        static UserSession()
        {
            Data = new SessionData();
            SessionFilePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSession.Session");
        }

        public static void Save()
        {
            using(StreamWriter file = File.CreateText(SessionFilePath))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                serializer.Serialize(file, Data);
            }
        }

        public static void Load()
        {
            if (!File.Exists(SessionFilePath)) return;
            using(StreamReader file = File.OpenText(SessionFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                Data = (SessionData)serializer.Deserialize(file, typeof(SessionData));
            }
        }
    }


    [JsonObject("SessionData")]
    public class SessionData
    {
        [JsonProperty("OpenedFiles", Order = 1)]
        public HashSet<String> OpenedFiles { get; set; }

        public SessionData()
        {
            OpenedFiles = new HashSet<string>();
        }
    }
}

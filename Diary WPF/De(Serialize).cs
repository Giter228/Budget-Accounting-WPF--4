using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Diary_WPF
{
    public class De_Serialize_
    {
        public static void Serialize<T>(List<T> notes, string way)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(way, json);
        }
        public static List<T> Deserialize<T>(string way)
        {
            List<T> list = new List<T>();

            Directory.CreateDirectory(way);
            way += "\\Budgets.json";

            if (!File.Exists(way))
            {
                File.Create(way);
            }

            string json = File.ReadAllText(way);
            if (json is "" or null)
            {
                Serialize(list, way);
                return list;
            }

            list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
    }
}

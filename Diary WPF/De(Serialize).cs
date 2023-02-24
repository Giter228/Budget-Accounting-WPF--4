using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Diary_WPF
{
    public class De_Serialize_
    {
        public static void Serialize<T>(List<T> notes, string way)
        {
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(way, json);
        }
        public static List<T> Deserialize<T>(string way, T example)
        {
            List<T> list = new List<T>();

            Directory.CreateDirectory(way);

            if (!File.Exists(way + "\\allNotes.json"))
            {
                way += "\\allNotes.json";
                File.Create(way);
            }
            else way += "\\allNotes.json";

            string json = File.ReadAllText(way);
            if (json is "" or null)
            {
                list.Add(example);
                Serialize(list, way);
                return list;
            }

            list = JsonConvert.DeserializeObject<List<T>>(json);
            return list;
        }
    }
}

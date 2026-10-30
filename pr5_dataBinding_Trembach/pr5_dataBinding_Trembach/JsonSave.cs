using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace pr5_dataBinding_Trembach
{
    public static class JsonSave
    {
        private static string file = "receipts.json";

        public static void Save(ObservableCollection<Receipt> list)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(list, options);
            File.WriteAllText(file, json);
        }

        public static ObservableCollection<Receipt> Load()
        {
            if (!File.Exists(file))
            {
                return new ObservableCollection<Receipt>();
            }

            string json = File.ReadAllText(file);
            ObservableCollection<Receipt> list = JsonSerializer.Deserialize<ObservableCollection<Receipt>>(json);
            return list;
        }
    }
}

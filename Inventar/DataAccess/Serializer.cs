using Inventar.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Inventar.DataAccess
{
    public static class Serializer
    {
        public static void Serialize<T>(IEnumerable<T> collection)
        {
            var file = typeof(T) == typeof(Product) ? "products.json" : "sold.json";
            var items = JsonConvert.SerializeObject(collection);
            File.WriteAllText(file, items );   
        }

        public static void Deserialize<T>(ICollection<T> collection)
        {
            var file = typeof(T) == typeof(Product) ? "products.json" : "sold.json";

            if (File.Exists(file))
            {
                var source = File.ReadAllText(file);
                var items = JsonConvert.DeserializeObject<ObservableCollection<T>>(source)!;

                foreach(var item in items)
                {
                    collection.Add(item);
                }
            }
        }
    }
}

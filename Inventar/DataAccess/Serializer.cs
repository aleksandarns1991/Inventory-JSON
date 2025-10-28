using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Inventar.DataAccess
{
    public static class Serializer
    {
        public static void Serialize<T>(ICollection<T> collection)
        {
            var file = $"{typeof(T).Name}.json";
            var items = JsonConvert.SerializeObject(collection);
            File.WriteAllText(file, items );   
        }

        public static void Deserialize<T>(ICollection<T> collection)
        {
            var file = $"{typeof(T).Name}.json";

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

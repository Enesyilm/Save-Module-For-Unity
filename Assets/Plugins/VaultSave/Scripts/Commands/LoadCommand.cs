using System.IO;
using Newtonsoft.Json;
using ValueSave.Interfaces;
using ValueSave.Systems;

namespace ValueSave.Commands
{ 
    public class LoadCommand:ILoader
    {
        public T Execute<T>(string path, SystemData password) where T : ISaveableEntity
        {
            string dataAsJson = File.ReadAllText(path);
            T objectToReturn = JsonConvert.DeserializeObject<T>(dataAsJson);
            return objectToReturn;
        }
        
    }
}
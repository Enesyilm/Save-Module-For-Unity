using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using VaultSave.Saver;
using VaultSave.Systems;

namespace VaultSave.Commands
{
    public class LoadCommand : ILoader
    {
        public T Execute<T>(string path, VSSystemData password)
        {
            string dataAsJson = File.ReadAllText(path);
            T objectToReturn = JsonConvert.DeserializeObject<T>(dataAsJson,
                new JsonSerializerSettings() { ObjectCreationHandling = ObjectCreationHandling.Replace });
            return objectToReturn;
        }
    }
}
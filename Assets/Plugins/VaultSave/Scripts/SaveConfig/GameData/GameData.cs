using System.Collections.Generic;
using Newtonsoft.Json;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public class GameData:ISaveableEntity
    {
        public int ExampleIntData=1; 
        public ExampleClass ExampleClass=new ExampleClass();
        [JsonProperty(ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<int> ballList=new List<int>(){1,1,1,2,4};
        public string ExampleStringData="GameData";
        public string GetKey()
        {
            throw new System.NotImplementedException();
        }
    }
}
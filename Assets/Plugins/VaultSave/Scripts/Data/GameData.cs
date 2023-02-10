using System.Collections.Generic;
using ValueSave.Interfaces;

namespace ValueSave.Data
{
    public class GameData:ISaveableEntity
    {
        public int ExampleIntData=1; 
        public SaveConfigData SaveConfigData=new SaveConfigData(new DatConfigData());
        public DenemeClass DenemeClass=new DenemeClass();
        public List<int> ballList=new List<int>(){1,1,1,2,4};
        public string ExampleStringData="GameData";
        public string GetKey()
        {
            throw new System.NotImplementedException();
        }
    }
}
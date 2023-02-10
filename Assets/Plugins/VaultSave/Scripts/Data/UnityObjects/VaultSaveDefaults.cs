using UnityEngine;
using ValueSave.Systems;

namespace ValueSave.Data
{
    [CreateAssetMenu(fileName = "VaultSave", menuName = "VaultSave", order = 0)]
    public class VaultSaveDefaults : ScriptableObject
    {
        public SystemData SystemData=new SystemData();
        public SaveConfigData SaveConfigData=new SaveConfigData(new JsonConfigData());
    }
}
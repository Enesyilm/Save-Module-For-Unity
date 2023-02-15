using UnityEngine;
using VaultSave.Systems;

namespace VaultSave.SaveConfig
{
    [CreateAssetMenu(fileName = "VaultSave", menuName = "VaultSave", order = 0)]
    public class VaultSaveDefaults : ScriptableObject
    {
        public SystemData SystemData=new SystemData();
        public SaveConfigData SaveConfigData=new SaveConfigData(new JsonConfigData());
    }
}
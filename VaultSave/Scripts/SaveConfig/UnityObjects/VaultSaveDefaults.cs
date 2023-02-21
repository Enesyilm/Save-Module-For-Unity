using UnityEngine;
using UnityEngine.Serialization;
using VaultSave.Systems;

namespace VaultSave.SaveConfig
{
    [CreateAssetMenu(fileName = "VaultSave", menuName = "VaultSave", order = 0)]
    public class VaultSaveDefaults : ScriptableObject
    {
        public VSSystemData vsSystemData=new VSSystemData();

        public VSSaveConfigData vsSaveConfigData=new VSSaveConfigData(new VSJsonConfigData());
    }
}
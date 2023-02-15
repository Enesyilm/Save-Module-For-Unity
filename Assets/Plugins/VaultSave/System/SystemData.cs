using System;
using UnityEngine.Serialization;
using VaultSave.AutoSave;

namespace VaultSave.Systems
{
    [Serializable]
    public class SystemData
    {
        public string Password="VaultSave";
        public bool AutoSave=true;
        public bool EncryptWithAes=false;
        public bool PrettyFormat=true;
        [FormerlySerializedAs("AutoSaveOn")] public AutoSaveTypes AutoSaveTypes;
    }
}
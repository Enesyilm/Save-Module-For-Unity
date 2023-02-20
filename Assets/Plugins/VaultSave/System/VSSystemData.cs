using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using VaultSave.AutoSave;

namespace VaultSave.Systems
{
    [Serializable]
    public class VSSystemData
    {
        public string Password="VaultSave";
        public bool AutoSave=true;
        public bool EncryptWithAes=false;
        public bool PrettyFormat=true;
        public List<String> AutoSaveKeys=new List<string>(){"key"};
        [FormerlySerializedAs("AutoSaveTypes")]
        public VSAutoSaveTypes vsAutoSaveTypes;
    }
}
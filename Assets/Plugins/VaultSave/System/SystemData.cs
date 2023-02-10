using System;

namespace ValueSave.Systems
{
    [Serializable]
    public class SystemData
    {
        public string Password="VaultSave";
        public bool AutoSave=true;
        public bool EncryptWithAes=false;
        public bool PrettyFormat=true;
    }
}
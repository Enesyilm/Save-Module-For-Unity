using VaultSave.Commands;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public class VSDatConfigData : VSISaveConfig
    {
        private const string _extention = ".dat";
        private ISaver _saver = new VSSaveJsonToAesCommand();
        private ILoader _loader = new VSLoadJsonToAesCommand();

        public string GetExtention()
        {
            return _extention;
        }

        public ISaver GetSaver()
        {
            return _saver;
        }

        public ILoader GetLoader()
        {
            return _loader;
        }
    }
}
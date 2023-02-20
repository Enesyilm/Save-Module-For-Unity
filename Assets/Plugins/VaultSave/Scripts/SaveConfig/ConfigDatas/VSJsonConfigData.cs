using VaultSave.Commands;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public class VSJsonConfigData : VSISaveConfig
    {
        private const string _extention = ".json";
        private ISaver _saver = new VSSaveCommand();
        private ILoader _loader = new LoadCommand();

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
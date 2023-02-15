using VaultSave.Commands;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    public class DatConfigData:ISaveConfig
    {
        private const string _extention = ".dat";
        private ISaver _saver = new SaveJsonToAesCommand();
        private ILoader _loader = new LoadJsonToAesCommand();
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
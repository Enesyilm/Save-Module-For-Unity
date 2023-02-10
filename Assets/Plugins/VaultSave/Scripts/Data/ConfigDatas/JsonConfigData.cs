
using ValueSave.Commands;
using ValueSave.Interfaces;

namespace ValueSave.Data
{
    public class JsonConfigData:ISaveConfig
    {
        private const string _extention = ".json";
        private ISaver _saver = new SaveCommand();
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
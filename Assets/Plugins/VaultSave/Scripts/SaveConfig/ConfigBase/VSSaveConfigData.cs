using System;
using UnityEngine;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    [Serializable]
    public class VSSaveConfigData
    {
        private VSISaveConfig _vsıSaveConfig;
        private readonly string _extention = ".json";
        public string FileName = "VaultSave";
        private string _path;
        private string _dataKey;


        public VSSaveConfigData(VSISaveConfig vsıSaveConfig)
        {
            _vsıSaveConfig = vsıSaveConfig;
            _extention = _vsıSaveConfig.GetExtention();
        }

        public string GetExtentention()
        {
            return _extention;
        }

        public string GetKeyName<T>(T dataToSave)
        {
            _dataKey = dataToSave.GetType().Name;
            return _dataKey;
        }

        public string GetPathName<T>(string saveKey, T dataToSave)
        {
            _path = Application.persistentDataPath + "/" + FileName + "/" + saveKey + dataToSave.GetType().Name +
                    _extention;
            return _path;
        }

        public string GetDirectoryPath()
        {
            return Application.persistentDataPath + "/" + FileName;
        }

        public ISaver GetSaver()
        {
            return _vsıSaveConfig.GetSaver();
        }

        public ILoader GetLoader()
        {
            return _vsıSaveConfig.GetLoader();
        }
    }
}
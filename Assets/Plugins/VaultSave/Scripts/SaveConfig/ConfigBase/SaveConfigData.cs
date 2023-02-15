using System;
using UnityEngine;
using VaultSave.Saver;

namespace VaultSave.SaveConfig
{
    [Serializable]
    public class SaveConfigData
    { 
        private ISaveConfig _saveConfig;
        private readonly string _extention = ".json"; 
        public string FileName = "VaultSave";
        private string _path;
        private string _dataKey;
        

        public SaveConfigData(ISaveConfig saveConfig)
        {
            _saveConfig = saveConfig;
            _extention=_saveConfig.GetExtention();
        }

        public string GetExtentention()
        {
            return _extention;
        }
        public string GetKeyName<T>(T dataToSave) where T : ISaveableEntity
        {
            _dataKey = dataToSave.GetType().Name;
            return _dataKey;
        }

        public string GetPathName<T>(T dataToSave) where T : ISaveableEntity
        {
            _path = Application.persistentDataPath + "/" + FileName+"/"+dataToSave.GetType().Name+_extention;
            return _path;
        }

        public string GetDirectoryPath()
        {
            return Application.persistentDataPath + "/" + FileName;
        }
        public ISaver GetSaver()
        {
            return _saveConfig.GetSaver();
        }

        public ILoader GetLoader()
        {
            return _saveConfig.GetLoader();
        }
       
    }
}
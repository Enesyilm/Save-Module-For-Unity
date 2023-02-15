using System.IO;
using UnityEngine;
using VaultSave.SaveConfig;
using VaultSave.Systems;


namespace VaultSave.Saver
{
    public class SaveController
    {
        #region Self Variables

        #region Private Variables

        private ISaver _saveCommand;
        private ILoader _loadCommand;
        private SaveConfigData _saveConfig;
        private string _path;
        private string _password;
        private SystemData _systemData;

        #endregion

        #endregion
        
        public SaveController(SaveConfigData saveConfigData, SystemData systemData)
        {
            _saveConfig = saveConfigData;
            _systemData = systemData;
        }
        

        public T PreLoadData<T>(T gameData) where T : ISaveableEntity
        {
            _path=_saveConfig.GetPathName(gameData);
            T dataInstance;
            if (!File.Exists(_path))
            {
                CreateDirectory();
                dataInstance=gameData;
                _saveConfig.GetSaver().Execute<T>(dataInstance,_path,_systemData);
                return dataInstance;
            }
            dataInstance=_saveConfig.GetLoader().Execute<T>(_path,_systemData);
            return dataInstance;
        }
        public void PreSaveData<T>(T gameData) where T : ISaveableEntity
        {
            CreateDirectory();
            _path=_saveConfig.GetPathName(gameData);
            _saveConfig.GetSaver().Execute<T>(gameData,_path,_systemData);
        }
        private void CreateDirectory()
        {
            if (!Directory.Exists(_saveConfig.GetDirectoryPath()))
            {
                Directory.CreateDirectory(_saveConfig.GetDirectoryPath());
            }
        }
        
    }
}

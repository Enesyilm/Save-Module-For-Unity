using UnityEngine;
using VaultSave.AutoSave;
using VaultSave.SaveConfig;

namespace VaultSave.Saver
{
    public static class SaveDistributor
    {
        private static VaultSaveDefaults _vaultSaveDefaults;

        private static GameData _gameData;
        
        private static SaveConfigData _datSaver=new SaveConfigData(new DatConfigData());
        private static SaveConfigData _jsonSaver=new SaveConfigData(new JsonConfigData());
        
        private static SaveController _saveController;
        private const string _autoSave = "VaultAutoSaver";

        static SaveDistributor()
        {
            InitAllConfigs();
            CheckAutoSave();
        }

        private static void CheckAutoSave()
        {
            if (_vaultSaveDefaults.SystemData.AutoSave)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = _autoSave;
                gameObject.AddComponent<VaultAutoSave>().SaveOn = _vaultSaveDefaults.SystemData.AutoSaveTypes;
            }
        }

        private static void InitConfiguration()
        {
            if (_vaultSaveDefaults.SystemData.EncryptWithAes)
            {
                InitWithEncrypt();
            }
            else if(! _vaultSaveDefaults.SystemData.EncryptWithAes)
            {
                InitWithoutEncrypt();
            }
            
            void InitWithoutEncrypt()
            {
                _vaultSaveDefaults.SaveConfigData = _jsonSaver;
            }
            void InitWithEncrypt()
            {
                _vaultSaveDefaults.SaveConfigData = _datSaver;

            }
        }
        
        private static void InitAllConfigs()
        {
            GetVaultDefaults();
            GetSaveData();
        }

        private static void GetVaultDefaults()
        { 
            _vaultSaveDefaults = Resources.Load<VaultSaveDefaults>("VaultSaveDefaults");
            InitConfiguration();
           _saveController = new SaveController(_vaultSaveDefaults.SaveConfigData,_vaultSaveDefaults.SystemData);
        }

        public static GameData GetSaveData()
        {
            GameData GetData()
            {
                return _saveController.PreLoadData(new GameData());
            }
            if (_gameData is null)
            {
                _gameData = GetData();
            }
            return _gameData;
        }
        
        public static void SaveData()
        {
            if (_gameData is null)GetSaveData();
            _saveController.PreSaveData(_gameData);
        }
        

    }
}
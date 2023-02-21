using UnityEngine;
using VaultSave.AutoSave;
using VaultSave.SaveConfig;

namespace VaultSave.Saver
{
    public static class SaveDistributor
    {
        private static VaultSaveDefaults _vaultSaveDefaults;

        //private static GameData _gameData;

        private static readonly VSSaveConfigData _datSaver = new VSSaveConfigData(new VSDatConfigData());
        private static VSSaveConfigData _jsonSaver = new VSSaveConfigData(new VSJsonConfigData());

        private static SaveController _saveController;
        private const string _autoSave = "VaultAutoSaver";

        static SaveDistributor()
        {
            InitAllConfigs();
            CheckAutoSave();
        }

        private static void CheckAutoSave()
        {
            if (_vaultSaveDefaults.vsSystemData.AutoSave)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = _autoSave;
                VSAutoSave vsAutoSave = gameObject.AddComponent<VSAutoSave>();
                vsAutoSave.SaveOn = _vaultSaveDefaults.vsSystemData.vsAutoSaveTypes;
            }
        }

        private static void InitConfiguration()
        {
            if (_vaultSaveDefaults.vsSystemData.EncryptWithAes)
            {
                InitWithEncrypt();
            }
            else if (!_vaultSaveDefaults.vsSystemData.EncryptWithAes)
            {
                InitWithoutEncrypt();
            }

            void InitWithoutEncrypt()
            {
                _vaultSaveDefaults.vsSaveConfigData = _jsonSaver;
            }

            void InitWithEncrypt()
            {
                _vaultSaveDefaults.vsSaveConfigData = _datSaver;
            }
        }

        private static void InitAllConfigs()
        {
            GetVaultDefaults();
        }

        private static void GetVaultDefaults()
        {
            _vaultSaveDefaults = Resources.Load<VaultSaveDefaults>("VaultSaveDefaults");
            InitConfiguration();
            _saveController = new SaveController(_vaultSaveDefaults.vsSaveConfigData, _vaultSaveDefaults.vsSystemData);
        }

        public static T GetSaveData<T>(string saveKey,T saveData)
        {
            T GetData()
            {
                return _saveController.EnsureLoadData(saveKey,saveData);
            }

            return GetData();
        }

        public static void SaveData<T>(string key,T saveData)
        {
            _saveController.EnsureSaveData(key,saveData);
        }
        public static void SaveData(string key)
        {
            _saveController.EnsureSaveAllData();
        }

        public static void SaveAutoData()
        {
            _saveController.EnsureSaveAllData();
        }
    }
}
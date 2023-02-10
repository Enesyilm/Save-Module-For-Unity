using UnityEngine;
using ValueSave.Data;

namespace ValueSave.Managers
{
    public class SaveDistributorManager:MonoBehaviour
    {
        private static GameData _gameData;
        private static SaveConfigData _datSaver=new SaveConfigData(new DatConfigData());
        private static SaveConfigData _jsonSaver=new SaveConfigData(new JsonConfigData());
        private static bool autoSave=true;
        private static bool EncryptWithAes=false;
        private static SaveManager _saveManager;
        private static VaultSaveDefaults _vaultSaveDefaults;


        private void Awake()
        {
            InitAllConfigs();
        }
        private static void InitConfiguration()
        {
            if (_vaultSaveDefaults.SystemData.EncryptWithAes)
            {
                InitWithEncrypt();
            }
            else if(!_vaultSaveDefaults.SystemData.EncryptWithAes)
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
        private void InitAllConfigs()
        {
            GetVaultDefaults();
            
            GetSaveData();
        }

        private static void GetVaultDefaults()
        { 
            _vaultSaveDefaults = Resources.Load<VaultSaveDefaults>("VaultSaveDefaults");
           autoSave=_vaultSaveDefaults.SystemData.AutoSave;
           EncryptWithAes=_vaultSaveDefaults.SystemData.EncryptWithAes;
           InitConfiguration();
           _saveManager = new SaveManager(_vaultSaveDefaults.SaveConfigData,_vaultSaveDefaults.SystemData);
        }

        public static GameData GetSaveData()
        {
            GameData GetData()
            {
                return _saveManager.PreLoadData(new GameData());
            }
            if (_gameData is null)
            {
                GetVaultDefaults();
                _gameData= GetData();
            }
            return _gameData;
        }
        
        public static void SaveData()
        {
            if (_gameData is null)GetSaveData();
            _saveManager.PreSaveData(_gameData);
        }
        
#if UNITY_EDITOR

        private void OnApplicationQuit()
        {
            if(autoSave)SaveData();
        }
#endif

#if UNITY_ANDROID && UNITY_IOS
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus&&autoSave)SaveData();
        }
#endif
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using VaultSave.SaveConfig;
using VaultSave.Systems;


namespace VaultSave.Saver
{
    public class SaveController
    {
        Assembly myAssembly ;
        
        private Dictionary<string, object> _savedClasses = new();

        private ISaver _saveCommand;

        private ILoader _loadCommand;

        private VSSaveConfigData _vsSaveConfig;

        private VSSystemData _vsSystemData;

        private string _path;
        private string _password;


        public SaveController(VSSaveConfigData vsSaveConfigData, VSSystemData vsSystemData)
        {
            _vsSaveConfig = vsSaveConfigData;
            _vsSystemData = vsSystemData;
        }
        public T EnsureLoadData<T>(string key, T gameData)
        {
            CheckSavedDataType(gameData);
            _path = _vsSaveConfig.GetPathName(key,gameData);
            T dataInstance;
            if (!File.Exists(_path))
            {
                CreateDirectory();
                _vsSaveConfig.GetSaver().Execute<T>(gameData, _path, _vsSystemData);
                _savedClasses.Add(key, gameData);
                return gameData;
            }

            if (_savedClasses.ContainsKey(key))
            {
                return (T)_savedClasses[key];
            }
            dataInstance = _vsSaveConfig.GetLoader().Execute<T>(_path, _vsSystemData);
            

            _savedClasses.Add(key, dataInstance);
            return dataInstance;
        }

        private static void CheckSavedDataType<T>(T gameData)
        {
            if (!gameData.GetType().IsClass)
            {
                throw new ArgumentException("Saved data must be class", nameof(gameData));
            }
        }

        public void EnsureSaveData<T>(string key, T gameData)
        {
            CreateDirectory();
            _path = _vsSaveConfig.GetPathName(key,gameData);
            CheckIfDataAlreadyExist(key, gameData);
        }

        private void CheckIfDataAlreadyExist<T>(string key, T gameData)
        {
            if (_savedClasses.ContainsKey(key))
            {
                _vsSaveConfig.GetSaver().Execute<T>((T)_savedClasses[key], _path, _vsSystemData);
            }
            else
            {
                _savedClasses.Add(key, gameData);
                _vsSaveConfig.GetSaver().Execute<T>(gameData, _path, _vsSystemData);
            }
        }

        public void EnsureSaveAllData()
        {
            CreateDirectory();
            foreach (var saved in _savedClasses)
            {
                _path = _vsSaveConfig.GetPathName(saved.Key,saved.Value);
                _vsSaveConfig.GetSaver().Execute(_savedClasses[saved.Key], _path, _vsSystemData);
            }
            
        }
       

        private void CreateDirectory()
        {
            if (!Directory.Exists(_vsSaveConfig.GetDirectoryPath()))
            {
                Directory.CreateDirectory(_vsSaveConfig.GetDirectoryPath());
            }
        }
    }
}
using System;
using UnityEngine;
using VaultSave.Saver;

namespace VaultSave.AutoSave
{
    public class VaultAutoSave : MonoBehaviour
    {
        public delegate void SaveData();
        
        private SaveData _saveDelegate=SaveDistributor.SaveData;
        
        [HideInInspector]
        public AutoSaveTypes SaveOn;
       
        private void Awake()
        {
            FireSaveDelegateIfMatched(AutoSaveTypes.Awake);
        }

        private bool HasState(AutoSaveTypes saveType)
        {
           
            return saveType == ( saveType & SaveOn);
        }

        private void FireSaveDelegateIfMatched(AutoSaveTypes saveType)
        {
            if (HasState(saveType))
            {
                _saveDelegate.Invoke();
            }
        }

        private void OnEnable()
        {
            FireSaveDelegateIfMatched(AutoSaveTypes.OnEnable);
        }

        private void Start()
        {
            FireSaveDelegateIfMatched(AutoSaveTypes.Start);
        }

        private void OnDestroy()
        {
            _saveDelegate = null;
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            FireSaveDelegateIfMatched(AutoSaveTypes.OnAppPause);
        }
        
        private void OnApplicationQuit()
        {
            FireSaveDelegateIfMatched(AutoSaveTypes.OnAppQuit);
        }
    }
}
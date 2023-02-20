using UnityEngine;
using VaultSave.Saver;

namespace VaultSave.AutoSave
{
    public class VSAutoSave : MonoBehaviour
    {
        [HideInInspector]
        public VSAutoSaveTypes SaveOn;


        public void SaveSelectedDatas()
        {
            Debug.Log("VAR");
            SaveDistributor.SaveAutoData();
        }

        private void Awake()
        {
            FireSaveDelegateIfMatched(VSAutoSaveTypes.Awake);
        }

        private bool HasState(VSAutoSaveTypes saveType)
        {
            return saveType == (saveType & SaveOn);
        }

        private void FireSaveDelegateIfMatched(VSAutoSaveTypes saveType)
        {
            if (HasState(saveType))
            {
                SaveSelectedDatas();
            }
        }

        private void OnEnable()
        {
            FireSaveDelegateIfMatched(VSAutoSaveTypes.OnEnable);
        }

        private void Start()
        {
            FireSaveDelegateIfMatched(VSAutoSaveTypes.Start);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            FireSaveDelegateIfMatched(VSAutoSaveTypes.OnAppPause);
        }

        private void OnApplicationQuit()
        {
            FireSaveDelegateIfMatched(VSAutoSaveTypes.OnAppQuit);
        }
    }
}
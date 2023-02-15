using UnityEngine;
using VaultSave.SaveConfig;
using VaultSave.Saver;

namespace VaultSave.Examples
{
    public class SaveTester : MonoBehaviour
    {
        private GameData _gameData; 
        public int _currentNumber;

        private void IncreaseAmount()
        {
            _gameData.ExampleIntData++;
        }

        private void Awake()
        {
            AssignSaveData();
        }

        private void Start()
        {
            IncreaseAmount();
        }

        private void Update()
        {            
            GetCurrentNumber();

        }

        private void AssignSaveData()
        {
            _gameData=SaveDistributor.GetSaveData();
        }

        private void GetCurrentNumber()
        {
            _currentNumber = _gameData.ExampleIntData;
        }
        
    }
}
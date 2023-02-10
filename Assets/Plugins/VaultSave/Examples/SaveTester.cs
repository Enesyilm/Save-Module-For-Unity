using UnityEngine;
using ValueSave.Data;
using ValueSave.Managers;

namespace ValueSave.Examples
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
            _gameData=SaveDistributorManager.GetSaveData();
        }

        private void GetCurrentNumber()
        {
            _currentNumber = _gameData.ExampleIntData;
        }
        
    }
}
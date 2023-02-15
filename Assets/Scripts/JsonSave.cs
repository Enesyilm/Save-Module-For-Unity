using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonSave : MonoBehaviour
{
    public class GameData
    {
        public int score;
        public string playerName;
    }

    public GameData gameData=new GameData();

    private void Start()
    {
        Load();
        Debug.Log("gameData.score"+gameData.score);
    }

    private void Awake()
    {
        //gameData.score = 130;
    }


    public void Save()
    {
        string dataAsJson = JsonConvert.SerializeObject(gameData);
        File.WriteAllText(Application.dataPath + "/gameData.json", dataAsJson);
        Debug.Log(Application.dataPath + "/gameData.json");
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/gameData.json"))
        {
            string dataAsJson = File.ReadAllText(Application.dataPath + "/gameData.json");
            gameData = JsonConvert.DeserializeObject<GameData>(dataAsJson);
        }
    }
#if UNITY_EDITOR

    private void OnApplicationQuit()
    {
        Save();
    }
#endif

#if UNITY_ANDROID && UNITY_IOS
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)SaveData();
        }
#endif
}
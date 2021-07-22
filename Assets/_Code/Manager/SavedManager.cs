using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SavedManager : MonoBehaviour
{
    public static SavedManager instance;
    public bool firstStartGame;
    public string playerTypeCharacter;
    public int countCoins, countSurvivedDays, countSurvivedWaveInCurretSurvivedDay, countKills;
    [System.Serializable]
    public struct Turrets
    {
        public int currentLevel;
        public string nameBuildTurret;
        public int idPlatform;
        public float timeBuildOrUpgrade;
    }
    public Turrets[] turrets;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    public void StartGame()
    {
        InvokeRepeating("Saved", 1f, 5f);
    }
    public void Load()
    {
        string json = ReadFromFile();
        LoadData loadData = JsonUtility.FromJson<LoadData>(json);
        firstStartGame = loadData.firstStartGame;
        playerTypeCharacter = loadData.playerTypeCharacter;
        countCoins = loadData.countCoins;
        countSurvivedDays = loadData.countSurvivedDays;
        countSurvivedWaveInCurretSurvivedDay = loadData.countSurvivedWaveInCurretSurvivedDay;
        countKills = loadData.countKills;
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].currentLevel = loadData.turrets[i].currentLevel;
            turrets[i].nameBuildTurret = loadData.turrets[i].nameBuildTurret;
            turrets[i].idPlatform = loadData.turrets[i].idPlatform;
            turrets[i].timeBuildOrUpgrade = loadData.turrets[i].timeBuildOrUpgrade;
        }
        //SaveDataZombie.instance.Load();
    }
    private string ReadFromFile()
    {
        using (StreamReader reader = new StreamReader(Application.persistentDataPath + "GameProgress.json"))
        {
            string json = reader.ReadToEnd();
            return json;
        }
    }

    public void Saved()
    {
        using (StreamWriter writer = new StreamWriter(Application.persistentDataPath + "GameProgress.json"))
        {
            writer.WriteLine(JsonUtility.ToJson(this));
            writer.Close();
        }    
    }
    public class LoadData
    {
        public bool firstStartGame;
        public int countCoins, countSurvivedDays, countSurvivedWaveInCurretSurvivedDay, countKills;
        public string playerTypeCharacter;
        [System.Serializable]
        public struct Turrets
        {
            public int currentLevel;
            public string nameBuildTurret;
            public int idPlatform;
            public float timeBuildOrUpgrade;
        }
        public Turrets[] turrets;
    }
}

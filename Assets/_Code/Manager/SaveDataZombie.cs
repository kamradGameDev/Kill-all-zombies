using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SaveDataZombie : MonoBehaviour
{
    public static SaveDataZombie instance;
    [System.NonSerialized] public static string dataPath;
    [System.Serializable]
    public struct ZombieInScene
    {
        public int id;
        public Vector3 lastPos;
        public float health;
    }
    public ZombieInScene[] zombieInScene;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        dataPath = Application.persistentDataPath + "SaveDataZombie.json";
        InvokeRepeating("Saved", 3f, 5f);
    }
    public void Load()
    {
        string json = ReadFromFile();
        LoadData loadData = JsonUtility.FromJson<LoadData>(json);
        for(int i = 0; i < zombieInScene.Length; i++)
        {
            zombieInScene[i].id = loadData.zombieInScene[i].id;
            zombieInScene[i].lastPos = loadData.zombieInScene[i].lastPos;
            zombieInScene[i].health = loadData.zombieInScene[i].health;
        }
        for(int i = 0; i < SpawnEnemyManager.instance.days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].countEnemiesInWaves; i++)
        {
            for(int j = 0; j < PoolManager.instance.zombies.Count; j++)
            {
                if (PoolManager.instance.zombies[j].id == zombieInScene[i].id)
                {
                    PoolManager.instance.zombies[j].transform.position = zombieInScene[i].lastPos;
                    PoolManager.instance.zombies[j].gameObject.SetActive(true);
                    PoolManager.instance.zombies[j].StartZombie(zombieInScene[i].health);
                    SpawnEnemyManager.instance.countLiveEnemiesInScene++;
                }
            }
        }
        Debug.Log("countLiveEnemiesInScene: " + SpawnEnemyManager.instance.countLiveEnemiesInScene);
        SpawnEnemyManager.instance.days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].countEnemiesInWaves -= SpawnEnemyManager.instance.countLiveEnemiesInScene;
    }

    private string ReadFromFile()
    {
        using (StreamReader reader = new StreamReader(dataPath))
        {
            string json = reader.ReadToEnd();
            return json;
        }
    }

    public void Saved()
    {
        using (StreamWriter writer = new StreamWriter(dataPath))
        {
            writer.WriteLine(JsonUtility.ToJson(this));
            writer.Close();
        }
    }
}
public class LoadData
{
    [System.Serializable]
    public struct ZombieInScene
    {
        public int id;
        public Vector3 lastPos;
        public float health;
    }
    public ZombieInScene[] zombieInScene;
}

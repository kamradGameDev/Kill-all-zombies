using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEnemyManager : MonoBehaviour
{
    public static SpawnEnemyManager instance;
    public int countLiveEnemiesInScene;
    public int countEnemisInCurrentWave;
    public bool startDay, startWave;
    [System.Serializable]
    public struct Days
    {   
        [System.Serializable]
        public struct Waves
        {
            public float intervalSpawnEnemies;
            public int countEnemiesInWaves;
            public string[] enemies;
            public Transform[] startPosEnemies;
        }
        public Waves[] waves;
    }
    public Days[] days;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    
    public IEnumerator InstanceNewZombiesInWave(string[] _array, List<Zombie> _pooled)
    {
        for (int i = 0; i < countEnemisInCurrentWave; i++)
        {
            for (int j = 0; j < _pooled.Count; j++)
            {
                if (_pooled[j].name == _array[i] && !_pooled[j].gameObject.activeInHierarchy)
                {
                    yield return new WaitForSeconds(days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].intervalSpawnEnemies);
                    NextActiveZombie(_pooled[j]);
                    Debug.Log("spawn zombie");
                }
            }
        }
    }
    private void NextActiveZombie(Zombie _zombie)
    {
        if(!WorldUIManager.instance.pauseGame)
        {
            _zombie.gameObject.SetActive(true);
            _zombie.StartZombie(_zombie.maxHealth);
            TargetUI.instance.targets.Add(_zombie.transform);
            int _rand = Random.Range(0, days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].startPosEnemies.Length);
            Vector3 _target = new Vector3()
            {
                x = days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].startPosEnemies[_rand].position.x,
                y = days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].startPosEnemies[_rand].position.y,
                z = 0
            };
            _zombie.transform.position = _target;
            //for(int i = 0; i < SaveDataZombie.instance.zombieInScene.Length; i++)
            //{
            //    if(SaveDataZombie.instance.zombieInScene[i].health.Equals(0))
            //    {
            //        SaveDataZombie.instance.zombieInScene[i].id = _zombie.id;
            //        SaveDataZombie.instance.zombieInScene[i].lastPos = _zombie.transform.position;
            //        SaveDataZombie.instance.zombieInScene[i].health = _zombie.health;
            //        countLiveEnemiesInScene = i + 1;
            //        break;
            //    }
            //} 
        }
    }
    public void StartDay(int _newDay)
    {
        int _nextDay = 1;
        _nextDay += _newDay;
        GameManager.instance.newDay.GetComponent<Text>().text = "New day: " + _nextDay;
        GameManager.instance.newDay.SetTrigger("Open");
        Invoke("StartWave", 2.5f);
        
    }
    public void StartWave()
    {
        //EventManager.instance.ChangeCoins(0, ' ');
        //Debug.Log("start wave");
        int _newWave = 1 + SavedManager.instance.countSurvivedWaveInCurretSurvivedDay;
        GameManager.instance.newDay.GetComponent<Text>().text = "New wave: " + _newWave;
        GameManager.instance.newDay.SetTrigger("Open");
        countEnemisInCurrentWave = days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].countEnemiesInWaves;
        StartCoroutine(InstanceNewZombiesInWave(days[SavedManager.instance.countSurvivedDays].waves[SavedManager.instance.countSurvivedWaveInCurretSurvivedDay].enemies, PoolManager.instance.zombies));
    }
}

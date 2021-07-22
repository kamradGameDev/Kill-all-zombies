using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Button startDayOrStartWave;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    public void PreStartGame()
    {
        StartStatusTurrets();
        GameManager.instance.StartGame();
    }
    public void StartGame()
    {
        EventManager.instance.StartGame(SavedManager.instance.countSurvivedDays);
        SavedManager.instance.StartGame();
        startDayOrStartWave.gameObject.SetActive(false);
        EventManager.instance.ChangeStatusWaveEvent += ChangeStatusWave;
    }
    public void StartNextDay()
    {
        SpawnEnemyManager.instance.StartDay(SavedManager.instance.countSurvivedDays);
        startDayOrStartWave.gameObject.SetActive(false);
    }
    public void StartNextWave()
    {
        SpawnEnemyManager.instance.StartWave();
        startDayOrStartWave.gameObject.SetActive(false);
    }
    private void StartStatusTurrets()
    {
        for (int i = 0; i < SavedManager.instance.turrets.Length; i++)
        {
            GameManager.instance.buyTurretManagers[i].buildTurretName = SavedManager.instance.turrets[i].nameBuildTurret;
            if (GameManager.instance.buyTurretManagers[i].currentBuildTurret)
            {
                GameManager.instance.buyTurretManagers[i].currentBuildTurret.currentLevel = SavedManager.instance.turrets[i].currentLevel;
            }
            GameManager.instance.buyTurretManagers[i].StartGame();
        }
        for (int i = 0; i < GameManager.instance.turrets.Length; i++)
        {
            if (GameManager.instance.turrets[i].gameObject.activeInHierarchy)
            {
                GameManager.instance.turrets[i].CurrentParameters();
            }
        }
        EventManager.instance.ChangeCountCoins(0, ' ');
    }
    private void ChangeStatusWave()
    {
        Debug.Log("countLiveEnemiesInScene: " + SpawnEnemyManager.instance.countLiveEnemiesInScene);
        Debug.Log("countEnemisInCurrentWave: " + SpawnEnemyManager.instance.countEnemisInCurrentWave);
        if (SpawnEnemyManager.instance.countLiveEnemiesInScene == 0 && SpawnEnemyManager.instance.countEnemisInCurrentWave <= 0)
        {
            if (SavedManager.instance.countSurvivedDays == SpawnEnemyManager.instance.days.Length &&
            SavedManager.instance.countSurvivedWaveInCurretSurvivedDay == SpawnEnemyManager.instance.days[SavedManager.instance.countSurvivedDays].waves.Length)
            {
                SavedManager.instance.countSurvivedDays -= 1;
                SavedManager.instance.countSurvivedWaveInCurretSurvivedDay = 0;
            }
            Debug.Log(SpawnEnemyManager.instance.days[SavedManager.instance.countSurvivedDays].waves.Length);
            if (SavedManager.instance.countSurvivedWaveInCurretSurvivedDay < SpawnEnemyManager.instance.days[SavedManager.instance.countSurvivedDays].waves.Length - 1)
            {
                SavedManager.instance.countSurvivedWaveInCurretSurvivedDay++;
                SavedManager.instance.Saved();
                startDayOrStartWave.onClick.AddListener(StartNextWave);
                startDayOrStartWave.gameObject.SetActive(true);
                startDayOrStartWave.transform.GetChild(0).GetComponent<Text>().text = "Start next wave";
                //SpawnEnemyManager.instance.StartWave();
            }
            else
            {
                GameManager.instance.countDaysText.text = (SavedManager.instance.countSurvivedDays + 1).ToString();
                SavedManager.instance.countSurvivedWaveInCurretSurvivedDay = 0;
                SavedManager.instance.countSurvivedDays += 1;
                startDayOrStartWave.onClick.AddListener(StartNextDay);
                startDayOrStartWave.gameObject.SetActive(true);
                startDayOrStartWave.transform.GetChild(0).GetComponent<Text>().text = "Start next day";
                //SpawnEnemyManager.instance.StartDay(SavedManager.instance.countSurvivedDays);
            }
        }
    }
}

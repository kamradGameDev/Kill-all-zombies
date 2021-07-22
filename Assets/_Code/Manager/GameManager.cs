using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameWindow;
    public DialogueManager dialogueManager;
    public ChoiceCharacterPlayer choiceCharacterPlayer;
    public Turret[] turrets;
    public BuyTurretManager[] buyTurretManagers;
    public DataTurret[] dataTurrets;
    public BuyTurretManager currentBuyTurretManager;
    public Text countCoinsText, plusCountCoinsText, countDaysText, countKillsZombieText;
    public Animator plusCountCoinsAnim;
    public Animator newDay;
    public GameObject plusDayEffectText;
    public Transform currentChoiseTurret;
    public int currentBuildId;
    public string choiseTurretName;
    public Animator nightMode;
    private static string dataPathSaveData;
    //public List<Action> updateStatusTurrets;
    
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        dataPathSaveData = Application.persistentDataPath + "GameProgress.json";
        CkeckLocalTime();
    }
    private void Start()
    {
        if (File.Exists(dataPathSaveData))
        {
            SavedManager.instance.Load();
            choiceCharacterPlayer.PreStartGame();
        }
        else
        {
            choiceCharacterPlayer.PreStartGame();
        }
        EventManager.instance.ChangeCountKillsEvent += ChangeCountKills;
        EventManager.instance.ChangeCountKills(0);
    }
    private void ChangeCountKills(int _newCount)
    {
        countKillsZombieText.text = "KIlls: " + _newCount + " / 500 000";
    }
    public void ChoiseTurret(string _turretName)
    {
        choiseTurretName = _turretName;
    }
    public void StartGame()
    {
        countCoinsText.text = SavedManager.instance.countCoins.ToString();
        countDaysText.text = "Survived days: " + SavedManager.instance.countSurvivedDays.ToString();
        plusCountCoinsText.text = "";
    }
    private void CkeckLocalTime()
    {
        int _nowHour = DateTime.Now.Hour;
        //Debug.Log("_nowHour: " + _nowHour);
        if(_nowHour >= 18 || _nowHour < 6)
        {
            if(!nightMode.GetCurrentAnimatorStateInfo(0).IsName("NightMode"))
            {
                //Debug.Log("NightMode");
                nightMode.SetTrigger("NightMode");
            }
        }
        else
        {
            if(!nightMode.GetCurrentAnimatorStateInfo(0).IsName("DayMode"))
            {
                //Debug.Log("DayMode");
                nightMode.SetTrigger("DayMode");
            }
        }    
    }

    
    //в это вскрипте добавить определение времени дня и ночи и включать в зависимости от этого
    //карту дня или ночи
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    public void StartGame(int _day)
    {
        SpawnEnemyManager.instance.StartDay(_day);
    }
    public event Action<int> ChangeCountCoinsEvent;
    public event Action<int> ChangeCountKillsEvent;
    public event Action ChangeStatusWaveEvent;
    public void ChangeStatusWave()
    {
        ChangeStatusWaveEvent?.Invoke();
    }
    public void ChangeCountKills(int _newCount)
    {
        SavedManager.instance.countKills += _newCount;
        ChangeCountKillsEvent?.Invoke(SavedManager.instance.countKills);
    }
    public void ChangeCountCoins(int _dropCoins, char _char)
    {
        string _newCoins = _dropCoins.ToString();
        if (_dropCoins == 0) { _newCoins = ""; }
        if (_dropCoins < 0) { _newCoins = _newCoins.Replace("-", ""); }
        SavedManager.instance.countCoins += _dropCoins;
        GameManager.instance.countCoinsText.text = SavedManager.instance.countCoins.ToString();
        GameManager.instance.plusCountCoinsText.text = _char + _newCoins;
        GameManager.instance.plusCountCoinsAnim.SetTrigger("Active");
        //ChangeStatusDayAndWave();
        if (ChangeCountCoinsEvent != null)
        {
            ChangeCountCoinsEvent(_dropCoins);
        }
        //SavedManager.instance.Saved();
    }
}

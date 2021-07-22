using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTurretHelper : MonoBehaviour
{
    private int idPlatform;
    private string buildAndUpgrageTurretName;
    private bool endTurret = true;
    private BuyTurretManager buyTurretManager;
    private DataTurret dataTurret;
    public float timeUpgrade;
    public Slider timeSlider;
    public void StartUpgrade(float _upgradeTime, DataTurret _dataTurret, bool _endTurret, BuyTurretManager _buyTurretManager, string _name, int _idPlatform)
    {
        idPlatform = _idPlatform;
        buildAndUpgrageTurretName = _name;
        buyTurretManager = _buyTurretManager;
        dataTurret = _dataTurret;
        timeUpgrade = _upgradeTime;
        timeSlider.value = 0;
        timeSlider.maxValue = timeUpgrade;
        endTurret = _endTurret;
    }
    private void Update()
    {
        if(!endTurret)
        {
            timeUpgrade -= Time.deltaTime;
            timeSlider.value += Time.deltaTime;
            if (timeSlider.value == timeSlider.maxValue)
            {
                endTurret = true;
                SavedManager.instance.turrets[idPlatform].currentLevel += 1;
                int _currentTypeLevel = (SavedManager.instance.turrets[idPlatform].currentLevel / 10) + 1;
                if (_currentTypeLevel <= 2)
                {
                    //buyTurretManager.buildBoard.SetActive(false);
                    
                    for(int i = 0; i < buyTurretManager.turrets.Length; i++)
                    {
                        if (buyTurretManager.turrets[i].name == buildAndUpgrageTurretName)
                        {
                            buyTurretManager.currentBuildTurret = buyTurretManager.turrets[i];
                            buyTurretManager.currentBuildTurret.currentLevel = SavedManager.instance.turrets[idPlatform].currentLevel + 1;
                            buyTurretManager.turrets[i].gameObject.SetActive(true);
                            buyTurretManager.buildTurretName = buildAndUpgrageTurretName;
                            buyTurretManager.turrets[i].CurrentParameters();
                        }
                    }
                }
                EventManager.instance.ChangeCountCoins(0, ' ');
                SavedManager.instance.turrets[idPlatform].nameBuildTurret = buildAndUpgrageTurretName;
                this.gameObject.SetActive(false);
                //SavedManager.instance.Saved();
            }
            else
            {
                buyTurretManager.levelUp.gameObject.SetActive(false);
                buyTurretManager.buildBoard.SetActive(false);
                SavedManager.instance.turrets[idPlatform].timeBuildOrUpgrade = Mathf.Round(timeUpgrade);
                //SavedManager.instance.Saved();
            }
        }
    }
}

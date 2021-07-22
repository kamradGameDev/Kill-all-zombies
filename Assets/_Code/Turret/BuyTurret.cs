using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurret : MonoBehaviour
{
    public DataTurret dataTurret;
    public Button buyButton;
    private void Start()
    {
        EventManager.instance.ChangeCountCoinsEvent += ChangeStatusButton;
    }
    private void ChangeStatusButton(int _count)
    {
        if(GameManager.instance.currentBuyTurretManager)
        {
            if (SavedManager.instance.countCoins >= dataTurret.upgradeLevelPriceTurrets[GameManager.instance.currentBuyTurretManager.currentLevel])
            {
                buyButton.interactable = true;
            }
            else
            {
                buyButton.interactable = false;
            }
        }
    }
    private void UpdateWindowParameters()
    {
        int _currentLevel = GameManager.instance.currentBuyTurretManager.currentLevel + 1;
        WorldUIManager.instance.currentLevel.text = "Level: " + _currentLevel + " + 1 of " + dataTurret.maxLevel;
        if (GameManager.instance.currentBuyTurretManager.currentLevel != 0)
        {
            if (GameManager.instance.currentBuyTurretManager.currentLevel < dataTurret.maxLevel)
            {
                WorldUIManager.instance.skillTurret.text = "Skill: " + dataTurret.skillLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1];
                WorldUIManager.instance.damageTurret.text = "Damage: " + dataTurret.damageLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1] + " > " + dataTurret.damageLevel[GameManager.instance.currentBuyTurretManager.currentLevel];
                WorldUIManager.instance.speedTurret.text = "Speed attack: " + dataTurret.speedAttackLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1] + " > " + dataTurret.speedAttackLevel[GameManager.instance.currentBuyTurretManager.currentLevel];
                WorldUIManager.instance.rangeTurret.text = "Range: " + dataTurret.rangeAttack[GameManager.instance.currentBuyTurretManager.currentLevel - 1] + " > " + dataTurret.rangeAttack[GameManager.instance.currentBuyTurretManager.currentLevel];
                WorldUIManager.instance.priceTurret.text = "Price: " + dataTurret.upgradeLevelPriceTurrets[GameManager.instance.currentBuyTurretManager.currentLevel];
            }
            else
            {
                WorldUIManager.instance.skillTurret.text = "Skill: " + dataTurret.skillLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1];
                WorldUIManager.instance.damageTurret.text = "Damage: " + dataTurret.damageLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1];
                WorldUIManager.instance.speedTurret.text = "Speed attack: " + dataTurret.speedAttackLevel[GameManager.instance.currentBuyTurretManager.currentLevel - 1];
                WorldUIManager.instance.rangeTurret.text = "Range: " + dataTurret.rangeAttack[GameManager.instance.currentBuyTurretManager.currentLevel - 1];
                WorldUIManager.instance.priceTurret.text = "Price: 0";
            }
        }
        else
        {
            //WorldUIManager.instance.currentLevel.text = "Level: 1";
            WorldUIManager.instance.skillTurret.text = "Skill: " + dataTurret.skillLevel[GameManager.instance.currentBuyTurretManager.currentLevel];
            WorldUIManager.instance.damageTurret.text = "Damage: " + dataTurret.damageLevel[0];
            WorldUIManager.instance.speedTurret.text = "Speed attack: " + dataTurret.speedAttackLevel[0];
            WorldUIManager.instance.rangeTurret.text = "Range: " + dataTurret.rangeAttack[0];
            WorldUIManager.instance.priceTurret.text = "Price: " + dataTurret.upgradeLevelPriceTurrets[GameManager.instance.currentBuyTurretManager.currentLevel];
        }
    }
    public void ChangeStateInWindow()
    {
        WorldUIManager.instance.buyTurret = this;
        WorldUIManager.instance.typeTurret.text = dataTurret.typeTurret;
        UpdateWindowParameters();
    }
    public void BuildTurret()
    {
        if(SavedManager.instance.countCoins >= dataTurret.upgradeLevelPriceTurrets[SavedManager.instance.turrets[GameManager.instance.currentBuildId].currentLevel])
        {
            EventManager.instance.ChangeCountCoins(-dataTurret.upgradeLevelPriceTurrets[SavedManager.instance.turrets[GameManager.instance.currentBuildId].currentLevel], '-');
            GameManager.instance.currentBuyTurretManager.upgradeTurretHelper.StartUpgrade(dataTurret.timeUpgrade[SavedManager.instance.turrets[GameManager.instance.currentBuildId].currentLevel], 
                dataTurret, false, GameManager.instance.currentBuyTurretManager, dataTurret.name, GameManager.instance.currentBuyTurretManager.idPlatfrorm);
            if(GameManager.instance.currentBuyTurretManager.currentBuildTurret)
            {
                GameManager.instance.currentBuyTurretManager.currentBuildTurret.gameObject.SetActive(false);
            }
            GameManager.instance.currentBuyTurretManager.upgradeTurretHelper.gameObject.SetActive(true);
            WorldUIManager.instance.ActiveTurretpanelAndDataTurret();
            WorldUIManager.instance.PassivePanelSelectTurret(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurretManager : MonoBehaviour
{
    public UpgradeTurretHelper upgradeTurretHelper;
    public int currentLevel;
    public int idPlatfrorm;
    public string buildTurretName;
    public Animator turretAnimator;
    public Animator levelUp;
    public Turret currentBuildTurret;
    public GameObject buildBoard;
    public Turret[] turrets;
    public void StartGame()
    {
        upgradeTurretHelper.transform.position = this.transform.position;
        for (int i = 0; i < this.transform.GetChild(1).GetChild(0).childCount; i++)
        {
            turrets[i] = this.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<Turret>();
        }
        EventManager.instance.ChangeCountCoinsEvent += CheckStatusThisTurret;
        CheckStatusThisTurret(0);
    }
    private void CheckStatusThisTurret(int _checkCoins)
    {
        if(buildTurretName == "")
        {
            //проверяем что у нас денег больше или равно для постройки туррели
            for(int i = 0; i < GameManager.instance.dataTurrets.Length; i++)
            {
                if(SavedManager.instance.countCoins >= GameManager.instance.dataTurrets[i].upgradeLevelPriceTurrets[0])
                {
                    buildBoard.SetActive(true);
                }
                else
                {
                    buildBoard.SetActive(false);
                }
            }
        }
        else
        {
            //уже что-то построено
            if(currentBuildTurret == null)
            {
                for (int j = 0; j < turrets.Length; j++)
                {
                    if (turrets[j].name == buildTurretName)
                    {
                        if (SavedManager.instance.turrets[idPlatfrorm].timeBuildOrUpgrade <= 0)
                        {
                            turrets[j].gameObject.SetActive(true);
                            currentBuildTurret = turrets[j];
                            currentLevel = SavedManager.instance.turrets[idPlatfrorm].currentLevel;
                            //turretAnimator.SetTrigger("IDle_" + SavedManager.instance.turrets[idPlatfrorm].currentLevel);
                        }
                        else
                        {
                            if(currentBuildTurret == null)
                            {
                                currentBuildTurret = turrets[j];
                            }
                            turrets[j].gameObject.SetActive(false);
                            buildBoard.SetActive(false);
                            upgradeTurretHelper.gameObject.SetActive(true);
                            upgradeTurretHelper.StartUpgrade(SavedManager.instance.turrets[idPlatfrorm].timeBuildOrUpgrade, currentBuildTurret.dataTurret, false, this, currentBuildTurret.name, idPlatfrorm);
                        }
                    }
                }
            }
            else
            {

                currentLevel = SavedManager.instance.turrets[idPlatfrorm].currentLevel;
                if (SavedManager.instance.turrets[idPlatfrorm].timeBuildOrUpgrade > 0)
                {
                    buildBoard.SetActive(false);
                    currentBuildTurret.gameObject.SetActive(false);
                    upgradeTurretHelper.gameObject.SetActive(true);
                }
            }
            if (SavedManager.instance.turrets[idPlatfrorm].timeBuildOrUpgrade <= 0)
            {
                if (currentBuildTurret.currentLevel < currentBuildTurret.dataTurret.maxLevel - 1)
                {
                    if (SavedManager.instance.countCoins >= currentBuildTurret.dataTurret.upgradeLevelPriceTurrets[currentBuildTurret.currentLevel])
                    {
                        levelUp.gameObject.SetActive(true);
                        levelUp.SetBool("Sprite", true);
                    }
                    else
                    {
                        if (levelUp.gameObject.activeInHierarchy && levelUp.GetCurrentAnimatorStateInfo(0).IsName("LoopSprite"))
                        {
                            levelUp.gameObject.SetActive(false);
                            levelUp.SetBool("Sprite", false);
                        }
                    }
                }
                else
                {
                    if (levelUp.gameObject.activeInHierarchy && levelUp.GetCurrentAnimatorStateInfo(0).IsName("LoopSprite"))
                    {
                        levelUp.SetBool("Sprite", false);
                    }
                }
            }
        }
    }
}

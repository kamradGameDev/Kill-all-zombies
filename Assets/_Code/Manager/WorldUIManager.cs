using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldUIManager : MonoBehaviour
{
    //public Transform[] upgradeSliders;
    public PlayerCharacter player;
    public Text warningText;
    public Animator warningAnim;
    public bool pauseGame = false;
    public Animator RangeAnim;
    public Animator pauseMenu;
    public static WorldUIManager instance;
    public Button attackButton, clickBuySelectTurret;
    public Image attackImgRollback;
    public BuyTurret buyTurret;
    public Animator panelBuyTurretSelect, buyTurretPanel;
    public bool statusWindowTurret = false;
    public Text typeTurret, damageTurret, speedTurret, rangeTurret, skillTurret, priceTurret, currentLevel;
    public void PauseStatusGame()
    {
        if (pauseGame)
        {
            for (int i = 0; i < PoolManager.instance.zombies.Count; i++)
            {
                PoolManager.instance.zombies[i].animator.speed = 0;
                PoolManager.instance.zombies[i].speedMove = 0f;
            }
        }
        else
        {
            for (int i = 0; i < PoolManager.instance.zombies.Count; i++)
            {
                PoolManager.instance.zombies[i].animator.speed = 1f;
                PoolManager.instance.zombies[i].speedMove = PoolManager.instance.zombies[i].dataZombie.speed;
            }
        }
    }
    public void ActiveTurretpanelAndDataTurret()
    {
        if(!buyTurretPanel.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            Debug.Log("active");
            statusWindowTurret = false;
            panelBuyTurretSelect.SetTrigger("Close");
            pauseGame = true;
            RangeAnim.SetTrigger("Active");
            PauseStatusGame();
            clickBuySelectTurret.onClick.RemoveAllListeners();
            clickBuySelectTurret.onClick.AddListener(buyTurret.BuildTurret);
            if (GameManager.instance.currentBuyTurretManager.buildTurretName == "")
            {
                clickBuySelectTurret.transform.GetChild(0).GetComponent<Text>().text = "Buy";
            }
            else
            {
                clickBuySelectTurret.transform.GetChild(0).GetComponent<Text>().text = "Update";
            }
            buyTurretPanel.SetTrigger("Open");
        }
    }
    public void PassivePanelSelectTurret(bool _status)
    {
        if(!_status)
        {
            pauseGame = false;
            PauseStatusGame();
            statusWindowTurret = false;
            RangeAnim.SetTrigger("Passive");
            if(!panelBuyTurretSelect.GetCurrentAnimatorStateInfo(0).IsName("Close"))
            {
                panelBuyTurretSelect.SetTrigger("Close");
            }

            buyTurretPanel.SetTrigger("Close");
        }
        else
        {
            RangeAnim.SetTrigger("Passive");
            statusWindowTurret = true;
            buyTurretPanel.SetTrigger("Close");
            panelBuyTurretSelect.SetTrigger("Open");
        } 
    }

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }
    public void PauseGame()
    {
        if(!pauseGame)
        {
            pauseGame = true;
            pauseMenu.SetTrigger("Open");
        }
        else
        {
            pauseGame = false;
            pauseMenu.SetTrigger("Close");
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            if (hit.collider != null && hit.transform.gameObject.tag == "TurretPos" && !buyTurretPanel.GetCurrentAnimatorStateInfo(0).IsName("Open") &&
                SavedManager.instance.turrets[hit.collider.GetComponent<BuyTurretManager>().idPlatfrorm].timeBuildOrUpgrade <= 0)
            {
                if (!statusWindowTurret && !EventSystem.current.IsPointerOverGameObject())
                {
                    if (/*hit.collider.GetComponent<BuyTurretManager>().buildTurretName == "" &&*/
                        Vector2.Distance(player.transform.position, hit.collider.transform.position) < 5f)
                    {
                        GameManager.instance.currentBuyTurretManager = hit.collider.GetComponent<BuyTurretManager>();
                        pauseGame = true;
                        PauseStatusGame();
                        GameManager.instance.currentBuildId = hit.collider.GetComponent<BuyTurretManager>().idPlatfrorm;
                        GameManager.instance.currentChoiseTurret = hit.transform;
                        panelBuyTurretSelect.GetComponent<RectTransform>().position = hit.transform.position;
                        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                        
                        panelBuyTurretSelect.SetTrigger("Open");
                        statusWindowTurret = true;
                    }
                    else
                    {
                        warningText.text = "You need to get closer to interact!";
                        warningAnim.SetTrigger("Active");
                    }
                }
            }
            else
            {
                if (hit.collider && hit.transform.gameObject.tag == "TurretPos" && SavedManager.instance.turrets[hit.collider.GetComponent<BuyTurretManager>().idPlatfrorm].timeBuildOrUpgrade > 0)
                {
                    warningText.text = "This tower is in the process of being updated!";
                    warningAnim.SetTrigger("Active");
                }
                if (!EventSystem.current.IsPointerOverGameObject() && !buyTurretPanel.GetCurrentAnimatorStateInfo(0).IsName("Open") &&
                    !panelBuyTurretSelect.GetCurrentAnimatorStateInfo(0).IsName("Start") && statusWindowTurret)
                {
                    Debug.Log("passive");
                    pauseGame = false;
                    PauseStatusGame();
                    if(!panelBuyTurretSelect.GetCurrentAnimatorStateInfo(0).IsName("Close"))
                    {
                        panelBuyTurretSelect.SetTrigger("Close");
                    }
                    statusWindowTurret = false;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceCharacterPlayer : MonoBehaviour
{
    public Button choiceButton;
    public string choiceName;
    public Animator animator;
    public GameObject[] playerCharacters;
    public GameObject[] charactersInMenu;
    public void PreStartGame()
    {
        if(SavedManager.instance.playerTypeCharacter == "")
        {
            GameManager.instance.dialogueManager.dialogueWindow.enabled = true;
            GameManager.instance.dialogueManager.StartDialogue(GameManager.instance.dialogueManager.startText);
            //StartStatusTurrets();
            this.GetComponent<Image>().enabled = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            for (int i = 0; i < playerCharacters.Length; i++)
            {
                if (playerCharacters[i].name == SavedManager.instance.playerTypeCharacter)
                {
                    GameManager.instance.gameWindow.SetActive(true);
                    animator.SetTrigger("Close");
                    playerCharacters[i].SetActive(true);
                    WorldUIManager.instance.player = playerCharacters[i].GetComponent<PlayerCharacter>();
                    Camera.main.GetComponent<CameraController>().target = WorldUIManager.instance.player.transform;
                    Camera.main.GetComponent<CameraController>().enabled = true;
                    LevelManager.instance.PreStartGame();
                    EventManager.instance.StartGame(SavedManager.instance.countSurvivedDays);
                    SavedManager.instance.StartGame();
                    //StartStatusTurrets();
                }
            }
        }
    }
    public void ChoiceCharacterInMenu(GameObject _obj)
    {
        for(int i = 0; i < charactersInMenu.Length; i++)
        {
            if(charactersInMenu[i] == _obj)
            {
                choiceName = _obj.name;
                SavedManager.instance.playerTypeCharacter = choiceName;
                choiceButton.interactable = true;
                if (charactersInMenu[i].transform.localScale == new Vector3(1f,1f,1f))
                {
                    charactersInMenu[i].transform.localScale += new Vector3(0.1f, 0.1f, 0f);
                }
            }
            else
            {
                if(charactersInMenu[i].transform.localScale != new Vector3(1f, 1f,1f))
                {
                    charactersInMenu[i].transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
                }
            }
        }
    }
    public void EndChoiceCharacter()
    {
        for (int i = 0; i < playerCharacters.Length; i++)
        {
            if (playerCharacters[i].name == choiceName)
            {
                GameManager.instance.gameWindow.SetActive(true);
                animator.SetTrigger("Close");
                playerCharacters[i].SetActive(true);
                WorldUIManager.instance.player = playerCharacters[i].GetComponent<PlayerCharacter>();
                LevelManager.instance.PreStartGame();
                if(SavedManager.instance.firstStartGame)
                {
                    EventManager.instance.StartGame(SavedManager.instance.countSurvivedDays);
                    SavedManager.instance.StartGame();
                }
            }
        }
        Camera.main.GetComponent<CameraController>().target = WorldUIManager.instance.player.transform;
        Camera.main.GetComponent<CameraController>().enabled = true;
        //StartStatusTurrets();
    }
    //private void StartStatusTurrets()
    //{
    //    for (int i = 0; i < SavedManager.instance.turrets.Length; i++)
    //    {
    //        GameManager.instance.buyTurretManagers[i].buildTurretName = SavedManager.instance.turrets[i].nameBuildTurret;
    //        if (GameManager.instance.buyTurretManagers[i].currentBuildTurret)
    //        {
    //            GameManager.instance.buyTurretManagers[i].currentBuildTurret.currentLevel = SavedManager.instance.turrets[i].currentLevel;
    //        }
    //        GameManager.instance.buyTurretManagers[i].StartGame();
    //    }
    //    for (int i = 0; i < GameManager.instance.turrets.Length; i++)
    //    {
    //        if (GameManager.instance.turrets[i].gameObject.activeInHierarchy)
    //        {
    //            GameManager.instance.turrets[i].CurrentParameters();
    //        }
    //    }
    //}
}

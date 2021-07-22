using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Animator loadScreen;
    public GameObject[] screens;
    public Image statusButtonSound, statusButtonMusic;
    public Sprite[] spriteStatus;
    public bool statusSound, statusMusic;
    private int nextScreen;
    private void Start()
    {
        if(PlayerPrefs.HasKey("statusSound"))
        {
            if (GetString("statusSound") == "true")
            {
                statusButtonSound.sprite = spriteStatus[0];
            }
            else
            {
                statusButtonSound.sprite = spriteStatus[1];
            }
        }
        if (PlayerPrefs.HasKey("statusMusic"))
        {
            if (GetString("statusMusic") == "true")
            {
                statusButtonMusic.sprite = spriteStatus[0];
            }
            else
            {
                statusButtonMusic.sprite = spriteStatus[1];
            }
        }
    }
    public void StartNextScreen(int _nextScreen)
    {
        nextScreen = _nextScreen;
        //LoadScreen.SetTrigger("Load");
        if(nextScreen == 0)
        {
            WorldUIManager.instance.pauseMenu.SetTrigger("Close");
        }
        loadScreen.SetTrigger("Load");
        Invoke("TimeLoad", 1f);
    }
    private void TimeLoad()
    {
        loadScreen.SetTrigger("Close");
        for (int i = 0; i < screens.Length; i++)
        {
            if(i == nextScreen)
            {
                screens[i].gameObject.SetActive(true);
            }
            else
            {
                screens[i].gameObject.SetActive(false);
            }
        }
        if(nextScreen == 0)
        {
            Camera.main.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            if(!SavedManager.instance.playerTypeCharacter.Equals(""))
            {
                WorldUIManager.instance.pauseGame = false;
                Camera.main.GetComponent<CameraController>().enabled = true;
            }
        }
    }
    public void ChangeStatusSound()
    {
        if(statusSound)
        {
            SetString("statusSound", "false");
            statusSound = false;
            statusButtonSound.sprite = spriteStatus[0];
        }
        else
        {
            SetString("statusSound", "true");
            statusSound = true;
            statusButtonSound.sprite = spriteStatus[1];
        }
    }
    public void ChangeStatusMusic()
    {
        if (statusMusic)
        {
            SetString("statusMusic", "false");
            statusMusic = false;
            statusButtonMusic.sprite = spriteStatus[0];
        }
        else
        {
            SetString("statusMusic", "true");
            statusMusic = true;
            statusButtonMusic.sprite = spriteStatus[1];
        }
    }
    private void SetString(string keyName, string value) => PlayerPrefs.SetString(keyName, value);
    private string GetString(string keyName) { return PlayerPrefs.GetString(keyName); }
}

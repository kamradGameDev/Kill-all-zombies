using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public MenuManager menuManager;
    public Image dialogueWindow;
    public Text dialogueText;
    public Text twiceTextObj;
    public string startText;
    public string endText;
    public enum StatusText
    {
        waithing, process, end
    }
    public StatusText statusText;
    public void StartDialogue(string _text)
    {
        statusText = StatusText.process;
        StartCoroutine(StartDialogueText(_text));
    }
    private IEnumerator StartDialogueText(string _text)
    {
        int _countText = 0;
        foreach(char c in _text)
        {
            if (_countText == _text.Length - 1)
            {
                twiceTextObj.color += new Color(0, 0, 0, 255);
                statusText = StatusText.end;
            }
            else
            {
                _countText++;
                dialogueText.text += c;
            }
            yield return new WaitForSeconds(0.015f);
        }
    }
    public void StartGame()
    {
        if(statusText == StatusText.end)
        {
            menuManager.loadScreen.SetTrigger("Close");
            statusText = StatusText.waithing;
            dialogueWindow.enabled = false;
            dialogueText.text = "";
            twiceTextObj.color = new Color(0,0,0,0);
            GameManager.instance.choiceCharacterPlayer.GetComponent<Image>().enabled = true;
            GameManager.instance.choiceCharacterPlayer.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

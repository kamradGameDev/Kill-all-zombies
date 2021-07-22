using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image joystickBG, joystick;
    public Vector2 inputVector;
    
    private void Start()
    {
        joystickBG = this.GetComponent<Image>();
        joystick = this.transform.GetChild(0).GetComponent<Image>();
    }
    //private void Update()
    //{
    //    if(!WorldUIManager.instance.statusWindowTurret && !player.statusAttack)
    //    {
    //        inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    //        player.animator.SetFloat("Horizontal", inputVector.x);
    //        player.animator.SetFloat("Vertical", inputVector.y);
    //        player.animator.SetFloat("Magnitude", inputVector.magnitude);
    //        player.rb2D.velocity = new Vector2(inputVector.x * player.speed, inputVector.y * player.speed);
    //    }
    //    //else
    //    //{
    //    //    player.rb2D.velocity = new Vector2(0,0);
    //    //    player.animator.SetBool("Walk", false);
    //    //}
    //}
    public void OnDrag(PointerEventData eventData)
    {
        if(!WorldUIManager.instance.player.stopMove && !WorldUIManager.instance.pauseGame && WorldUIManager.instance.attackImgRollback.fillAmount == 0)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
                pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);
            }
            inputVector = new Vector2(pos.x, pos.y);
            inputVector = (inputVector.sqrMagnitude > 1.0f * 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 3),
                inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 3));
            WorldUIManager.instance.player.animator.SetFloat("Horizontal", inputVector.x);
            WorldUIManager.instance.player.animator.SetFloat("Vertical", inputVector.y);
            WorldUIManager.instance.player.animator.SetFloat("Magnitude", inputVector.magnitude);

            WorldUIManager.instance.player.rb2D.velocity = new Vector2(inputVector.x * WorldUIManager.instance.player.speed, inputVector.y * WorldUIManager.instance.player.speed);
            WorldUIManager.instance.player.characterDirection = new Vector2(inputVector.x, inputVector.y);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;

        WorldUIManager.instance.player.animator.SetFloat("Horizontal", inputVector.x);
        WorldUIManager.instance.player.animator.SetFloat("Vertical", inputVector.y);
        WorldUIManager.instance.player.animator.SetFloat("Magnitude", inputVector.magnitude);

        joystick.rectTransform.anchoredPosition = Vector2.zero;
        WorldUIManager.instance.player.rb2D.velocity = new Vector2(0f, 0f);
    }
}

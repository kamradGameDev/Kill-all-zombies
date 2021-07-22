using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : PlayerCharacter
{   
    private void Start()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).gameObject.activeSelf)
            {
                activePlayer = this.transform.GetChild(i).gameObject;
                break;
            }
        }
        startHpBarScale = hpBar.localScale.x;
        
        WorldUIManager.instance.attackButton.onClick.AddListener(StartAttack);
    }
    private void StartAttack()
    {
        closest = FindClosestGameObject();
        if (closest && closest.CompareTag("Enemy") && !WorldUIManager.instance.pauseGame && WorldUIManager.instance.attackImgRollback.fillAmount == 0)
        {
            statusAttack = true;
            AttackRange(closest.transform.position, activePlayer.transform.position);
            Vector2 _vector = closest.transform.position - this.transform.position;
            animator.SetFloat("VerAttack", _vector.y);
            animator.SetFloat("HorAttack", _vector.x);
            animator.SetFloat("AttackMagnitude", _vector.magnitude);
            WorldUIManager.instance.attackImgRollback.fillAmount = 1f;
        }
    }
    
    private void Update()
    {
        if (WorldUIManager.instance.attackImgRollback.fillAmount > 0)
        {
            WorldUIManager.instance.attackImgRollback.fillAmount -= Time.deltaTime / delayAttack;
        }
    }
    public void AttackRange(Vector3 _vector, Vector2 _startPos)
    {
        closest.GetComponent<Zombie>().Damage(damage);
    }
    private void EndAttack()
    {
        statusAttack = false;
        animator.SetFloat("HorAttack", 0);
        animator.SetFloat("VerAttack", 0);
        animator.SetFloat("AttackMagnitude", 0);
    }
}

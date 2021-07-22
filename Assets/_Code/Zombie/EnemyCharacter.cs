using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCharacter : MonoBehaviour
{
    public int id;
    public bool explosionType = false;
    public DataZombie dataZombie;
    public Transform hpBar;
    public Transform closest;
    public Rigidbody2D rb2D;
    public float speedMove;
    public float attackDistance, findZone;
    public Animator animator;
    public float health, maxHealth;
    public float damage;
    public SpriteRenderer spriteRenderer;
    [SerializeField]protected float timeAttack, maxTimeAttack = 1f;
    protected bool hit;
    public int dropCoins;
    public IEnumerator FindTarget()
    {
        while (WorldUIManager.instance.player)
        {
            if(Vector2.Distance(this.transform.position, WorldUIManager.instance.player.transform.position) <= findZone)
            {
                closest = WorldUIManager.instance.player.transform;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void DamageClosest()
    {
        Debug.Log("damage player");
        closest.GetComponent<PlayerCharacter>().Damage(damage);
    }
    protected void Flip()
    {
        if(this.transform.position.x > closest.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}

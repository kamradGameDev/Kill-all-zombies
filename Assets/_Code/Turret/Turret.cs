using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public DataTurret dataTurret;
    public readonly CircleCollider2D[] collidersNonAlloc = new CircleCollider2D[20];
    public LayerMask enemyMask;
    public float startRotateZ;
    public int currentLevel = 0;
    public int typeLevel;
    public float damage = 2f;
    public float timeAttack = 1f;
    public float distanceAttack = 2f;
    public float standartTimeAttack = 1f;
    public float speedRotation = 3f;
    public Animator animator;
    public Zombie closest;
    public GameObject[] targetRotate;
    public int indexRotate;
    public Vector3 direction;
    public bool findStatus = false;
    public Vector3 Direction(GameObject target)
    {
        direction = target.transform.position - this.transform.position;
        return direction;
    }
    public void CurrentParameters()
    {
        typeLevel = (currentLevel / 10) + 1;
        if(typeLevel > 2)
        {
            typeLevel = 2;
        }
        animator.SetTrigger("Idle_Level_" + typeLevel);
        damage = dataTurret.damageLevel[currentLevel];
        timeAttack = dataTurret.speedAttackLevel[currentLevel];
        distanceAttack = dataTurret.rangeAttack[currentLevel];
    }
    public void Attack()
    {
        timeAttack = standartTimeAttack;
        animator.SetTrigger("Attack_" + typeLevel);
    }
    public void Damage()
    {
        if(closest)
        {
            closest.GetComponent<Zombie>().Damage(damage);
        }
    }
    public IEnumerator FindEnemies()
    {
        while (true)
        {
            var _size = Physics2D.OverlapCircleNonAlloc(this.transform.position, distanceAttack, collidersNonAlloc, enemyMask);
            for (int i = 0; i < _size; i++)
            {
                var _enemy = collidersNonAlloc[i].GetComponent<Zombie>();
                if (_enemy && _enemy.health > 0)
                {
                    closest = _enemy;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

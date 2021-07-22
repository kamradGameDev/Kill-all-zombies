using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : MonoBehaviour
{
    protected float startHpBarScale;
    public bool statusAttack = false;
    public Transform hpBar;
    public bool stopMove = false;
    public LayerMask enemyMask;
    public Transform closest;
    public float attackDistance = 20f;
    public GameObject activePlayer;
    public GameObject attackObjPrefab;
    public Vector3 characterDirection;
    public float thrust = 20f;
    public Rigidbody2D rb2D;
    public Animator animator;
    public SpriteRenderer characterSpriteRenderer;
    public float speed;
    public float health, maxHealth;
    public float damage;
    public float defence;
    public float delayAttack;
    public enum TypeCharacter
    {
        melee, range, healer
    }
    public TypeCharacter typeCharacter;
    protected Transform FindClosestGameObject()
    {
        GameObject[] _targets = GameObject.FindGameObjectsWithTag("Enemy");
        return FindNearestClosest(_targets);
    }
    private Transform FindNearestClosest(GameObject[] _targets)
    {
        Vector3 _curPos = this.transform.position;
        for(int i = 0; i < _targets.Length; i++)
        {
            var _enemy = _targets[i].GetComponent<EnemyCharacter>();
            if (_enemy.health > 0)
            {
                Vector3 _diff = _targets[i].transform.position - _curPos;
                float _curDist = _diff.sqrMagnitude;
                if(_curDist < attackDistance)
                {
                    return _targets[i].transform;
                }
            }
        }
        return this.transform;
    }
    public void Damage(float _damage)
    {
        if (health > 0)
        {
            animator.SetTrigger("Hit");
            health -= _damage;

        }
        hpBar.localScale = new Vector2(health * startHpBarScale / maxHealth, 1f);
        if (hpBar.localScale.x < 0)
        {
            hpBar.localScale = new Vector2(0, 1);
        }
    }
}

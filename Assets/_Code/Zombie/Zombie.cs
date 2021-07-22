using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class Zombie : EnemyCharacter
{
    public ParticleSystem explosion;
    private float startHpBarScale;
    private Path path;
    private int currentWayPoint = 0;
    [SerializeField]private Seeker seeker;
    public float nextWaypointsDistance = 1f;
    [SerializeField] private bool distanceToPlayerZoneAttack;
    public void StartZombie(float _startHealth)
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        rb2D.constraints = RigidbodyConstraints2D.None;
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        health = _startHealth;
        closest = GameObject.Find("TargetZombie").transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);

        startHpBarScale = 73;
        hpBar.localScale = new Vector2(health * startHpBarScale / maxHealth, 1f);
        StartCoroutine(FindTarget());
        //InvokeRepeating("SendToSaveDataZombie", 2f, 3f);
    }
    private void SendToSaveDataZombie()
    {
        for (int i = 0; i < SaveDataZombie.instance.zombieInScene.Length; i++)
        {
            if (id == SaveDataZombie.instance.zombieInScene[i].id)
            {
                if (health > 0)
                {
                    SaveDataZombie.instance.zombieInScene[i].lastPos = this.transform.position;
                    SaveDataZombie.instance.zombieInScene[i].health = this.health;
                }
                else
                {
                    SaveDataZombie.instance.zombieInScene[i].id = 999;
                    SaveDataZombie.instance.zombieInScene[i].lastPos = new Vector3(0, 0, 0);
                    SaveDataZombie.instance.zombieInScene[i].health = 0;
                }
            }
        }
        SaveDataZombie.instance.Saved();
    }
    private void UpdatePath()
    {
        seeker.StartPath(rb2D.position, closest.transform.position, OnPathComplete);
    }
    private void PassiveObj()
    {
        this.gameObject.SetActive(false);
    }
    public void ExplosionZombie()
    {
        explosion.Play();
    }
    private void PostDie()
    {
        SpawnEnemyManager.instance.countLiveEnemiesInScene--;
        //Debug.Log("countLiveEnemiesInScene: " + SaveDataZombie.countLiveEnemiesInScene);
        EventManager.instance.ChangeCountKills(1);
        this.transform.GetChild(0).gameObject.SetActive(false);
        if(explosionType && Vector2.Distance(this.transform.position, WorldUIManager.instance.player.transform.position) <= attackDistance)
        {
            DamageClosest();
        }
        Invoke("PassiveObj", 5f);
    }
    private void EndHit()
    {
        hit = false;
    }
    public void Damage(float _damage)
    {
        float _health = health - _damage;
        if(_health > 0)
        {
            //for (int i = 0; i < SaveDataZombie.instance.zombieInScene.Length; i++)
            //{
            //    if (id == SaveDataZombie.instance.zombieInScene[i].id)
            //    {
            //        SaveDataZombie.instance.zombieInScene[i].health = this.health;
            //    }
            //}
            health = _health;
            if (!distanceToPlayerZoneAttack)
            {
                hit = true;
                animator.SetTrigger("Hit");
            }
        }
        else
        {
            health = 0;
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            SpawnEnemyManager.instance.countEnemisInCurrentWave--;
            EventManager.instance.ChangeCountCoins(dropCoins, '+');
            animator.SetTrigger("Die");
            TargetUI.instance.targets.Remove(this.transform);
        }
        hpBar.localScale = new Vector2(health * startHpBarScale / maxHealth, 1f);
        if (hpBar.localScale.x < 0)
        {
            hpBar.localScale = new Vector2(0, 1);
        }
    }
    private void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    
    private void FixedUpdate()
    {
        if(!WorldUIManager.instance.pauseGame)
        {
            if (health > 0)
            {
                Flip();
                if (path == null)
                {
                    return;
                }
                if (currentWayPoint >= path.vectorPath.Count)
                {
                    if(closest == WorldUIManager.instance.player.transform)
                    {
                        distanceToPlayerZoneAttack = true;
                        if(timeAttack > 0)
                        {
                            timeAttack -= Time.deltaTime;
                        }
                        if(timeAttack <= 0)
                        {
                            timeAttack = maxTimeAttack;
                            Debug.Log("Attack");
                            Attack();
                        }
                    }
                    else if(closest != WorldUIManager.instance.player.transform)
                    {
                        distanceToPlayerZoneAttack = false;
                    }
                    return;
                }
                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb2D.position).normalized;
                Vector2 force = direction * speedMove * Time.fixedDeltaTime;

                rb2D.AddForce(force, ForceMode2D.Force);
                if(!hit && !distanceToPlayerZoneAttack)
                {
                    distanceToPlayerZoneAttack = false;
                    animator.SetBool("Walk", true);
                }

                float distance = Vector2.Distance(rb2D.position, path.vectorPath[currentWayPoint]);
                if (distance < nextWaypointsDistance)
                {
                    currentWayPoint++;
                }
            }
            else
            {
                seeker.enabled = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Turret
{
    private void Start()
    {
        indexRotate = Random.Range(0,2);
        StartCoroutine(FindEnemies());
    }
    private void Update()
    {
        if(!WorldUIManager.instance.pauseGame)
        {
            if (closest && closest.health > 0)
            {
                direction = Direction(closest.gameObject);
                this.transform.up = Vector2.MoveTowards(this.transform.up, direction, Time.deltaTime * speedRotation);
                if (timeAttack > 0)
                {
                    timeAttack -= Time.deltaTime;
                }
                else
                {
                    Attack();
                }
            }
            else
            {
                direction = Direction(targetRotate[indexRotate]);
                float _angle = Vector2.Angle(this.transform.up, direction);
                if (_angle > 2)
                {
                    this.transform.up = Vector2.Lerp(this.transform.up, direction, Time.deltaTime * speedRotation / 5);
                }
                else
                {
                    if (indexRotate == 0) { indexRotate = 1; }
                    else { indexRotate = 0; }
                }
            }
        }
    }
}

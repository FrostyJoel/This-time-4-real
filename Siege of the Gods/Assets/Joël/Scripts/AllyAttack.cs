using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAttack : Tower
{
    public float health;
    public float reached = 1f;
    public void Update()
    {
        Timer();
        Attack();
        if (health <= 0)
        {
            GetComponentInParent<AllyTargeting>().SelfDestroy();
        }
    }
    public void Attack()
    {
        if (targetList.Count > 0 && cooldown <= 0)
        {
            foreach (GameObject enemies in targetList)
            {
                float dis = Vector3.Distance(transform.position, enemies.transform.position);
                if (dis <= rad)
                {
                    enemies.GetComponent<TestWay>().health -= amountOfDamage;
                }
            }
            cooldown = maxCooldown;
        }
    }
}

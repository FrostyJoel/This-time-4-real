using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOITower : Tower
{
    public void AOIDamage(int amountofdamage)
    {
        if (cooldown <= 0)
        {
            foreach (GameObject enemies in targetList)
            {
                enemies.GetComponent<TestWay>().health -= amountofdamage;
                cooldown = maxCooldown;
            }
        }
    }

    public void AOIPoison(float poisonTick, int amountofdamage)
    {
        if (cooldown <= 0)
        {
            foreach (GameObject enemies in targetList)
            {
                    if (enemies.GetComponent<TestWay>().tickDamage == false)
                    {
                        enemies.GetComponent<TestWay>().tickDamage = true;
                        enemies.GetComponent<TestWay>().amountOfPoisonDamage += amountofdamage;
                        enemies.GetComponent<TestWay>().lengthOfPoision += poisonTick;
                    }
                cooldown = maxCooldown;
            }
        }
    }
}

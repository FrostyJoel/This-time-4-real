using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : AOITower
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        AOIDamage(amountOfDamage);
        AttackTarget();
    }

    public void AttackTarget()
    {
        foreach (GameObject enemy in targetList)
        {
            if(enemy != null)
            {
                enemy.GetComponent<TestWay>().fakeWaypoint = GetComponentInParent<Transform>();
            }

        }
    }
}

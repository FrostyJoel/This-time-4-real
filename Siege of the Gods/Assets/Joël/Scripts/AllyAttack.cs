using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyAttack : Tower
{
    public float health;
    public float maxhealth;
    public float reached = 1f;

    public GameObject mainTower;

    public Image healthImage;
    public Image childImage;

    private void Start()
    {
        health = maxhealth;
    }
    public void Update()
    {
        healthImage.transform.LookAt(Camera.main.transform.position);
        childImage.fillAmount = health / maxhealth;
        Timer();
        if (targetList.Count > 0)
        {
            AttackTarget();
            Attack();
        }
        if (health <= 0)
        {
            SelfDestroy();
        }
    }

    public void AttackTarget()
    {
        foreach (GameObject enemy in targetList)
        {
            enemy.GetComponent<TestWay>().currentWaypoint = GetComponentInChildren<Transform>();
            enemy.GetComponent<TestWay>().fakeWaypoint = GetComponentInChildren<Transform>();
        }
    }

    public void SelfDestroy()
    {
        mainTower.GetComponent<AllySpawnerTower>().spawned = false;
        Destroy(gameObject);
    }

    public void Attack()
    {
        if (cooldown <= 0)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyTargeting : Tower
{
    public GameObject mainTower;
    public List<Transform> enemyPos = new List<Transform>();

    private void Start()
    {
        range = GetComponent<SphereCollider>();
        range.radius = rad;
    }

    public void AttackTarget()
    {
        foreach (GameObject enemy in targetList)
        {
            enemy.GetComponent<TestWay>().currentWaypoint = GetComponentInChildren<Transform>();
            enemy.GetComponent<TestWay>().fakeWaypoint = GetComponentInChildren<Transform>();
        }
    }
    public void Update()
    {
        if (targetList.Count > 0)
        {
            AttackTarget();
        }
    }
    public void SelfDestroy()
    {
        mainTower.GetComponent<AllySpawnerTower>().spawned = false;
        Destroy(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float timeOfDeath = 2f;
    public UIManager ui;
    public GameObject homebase;

    private void Awake()
    {
        homebase = GameObject.FindGameObjectWithTag("Base");
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        CheckPoison();
        GameOver();
    }

    public void CheckPoison()
    {
        foreach (GameObject enemy in enemies)
        {
            TestWay testEnemy = enemy.GetComponent<TestWay>();
            if (testEnemy.tickDamage)
            {
                if (testEnemy.lengthOfPoision > 0)
                {
                    testEnemy.lengthOfPoision -= Time.deltaTime * UIManager.gameSpeed;

                    StartCoroutine(testEnemy.Poisoning());
                }
                else if (testEnemy.lengthOfPoision < 0)
                {
                    testEnemy.tickDamage = false;
                    testEnemy.amountOfPoisonDamage = 0;
                    StopCoroutine(testEnemy.Poisoning());
                }
            }
        }
    }

    public void CheckDeath()
    {
        foreach (GameObject enemy in enemies)
        {
            TestWay testEnemy = enemy.GetComponent<TestWay>();
            EnemyDeath death = enemy.GetComponent<EnemyDeath>();
            if (testEnemy.health <= 0)
            {
                ui.totalMoney += testEnemy.money;
                if(death.amountOfEnemies > 0)
                {
                    for (int i = 0; i < death.amountOfEnemies; i++)
                    {
                        GameObject spawn = Instantiate(death.spawn,death.extraEnemyPos[i].position,Quaternion.identity);
                        spawn.GetComponent<TestWay>().goal = testEnemy.goal;
                    }
                }
                //testEnemy.ResetAnime();
                //enemy.GetComponent<Animator>().SetTrigger("isDying");
                testEnemy.alive = false;
                enemies.Remove(enemy);
                Destroy(enemy, timeOfDeath);
                break;
            }
        }
    }
    public void GameOver()
    {
        Base b = homebase.GetComponent<Base>();
        if(b.health <= 0)
        {
            UIManager.gameSpeed = 0;
        }
    }
}

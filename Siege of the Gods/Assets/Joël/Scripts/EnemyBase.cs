using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float movespeed;
    public float maxMovespeed;
    public float cooldown, maxCooldown;
    public float lengthOfPoision = 0f;
    private float tick;
    public float maxTick = 2f;

    public int amountOfPoisonDamage = 0;
    public int health;
    public int damage;
    public int money;

    public bool alive = true;
    public bool tickDamage = false;

    public Animator anime;

    public IEnumerator Poisoning()
    {
        while (true)
        {
            if (tickDamage)
            {
                tick -= Time.deltaTime * UIManager.gameSpeed * maxTick;
                if(tick < 0)
                {
                    print("Poisoned");
                    health -= amountOfPoisonDamage;
                    tick = maxTick;
                }
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void Timer()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime * UIManager.gameSpeed;
        }
    }

}

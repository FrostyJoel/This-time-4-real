using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public float movespeed;
    public float maxMovespeed;
    public float cooldown, maxCooldown;
    public float lengthOfPoision = 0f;
    private float tick;
    public float maxTick = 2f;

    public float health;
    public float maxhealth;
    public float damage;
    public float amountOfPoisonDamage = 0;

    public int money;

    public bool alive = true;
    public bool tickDamage = false;

    public Animator anime;
    public Image healthImage;
    public Image childImage;
    PoisonManager p;

    void Awake()
    {
        health = maxhealth;
        //anime = GetComponent<Animator>(); 
        p = GameObject.FindGameObjectWithTag("Manager").GetComponent<PoisonManager>();
        movespeed = maxMovespeed;
        p.enemies.Add(gameObject);
    }
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

    public virtual void Update()
    {
        //anime.speed = UIManager.gameSpeed;
        healthImage.transform.LookAt(Camera.main.transform.position);
        childImage.fillAmount = health / maxhealth;
        Timer();
    }

    public void Timer()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime * UIManager.gameSpeed;
        }
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float rad;
    public float maxCooldown;
    public float cooldown;

    public int amountOfDamage;

    public PoisonManager manager;
    public List<GameObject> targetList = new List<GameObject>();
    public SphereCollider range;

    public void Awake()
    {
        range = GetComponent<SphereCollider>();
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PoisonManager>();
        range.radius = rad;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            targetList.Add(other.transform.gameObject);
        }
    }
    public virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            if (other.gameObject.GetComponent<TestWay>().alive == false)
            {
                targetList.Remove(other.transform.gameObject);
            }
        }
    }
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == enemyTag)
        {
            targetList.Remove(other.transform.gameObject);
        }
    }

    public void Timer()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime * UIManager.gameSpeed;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}

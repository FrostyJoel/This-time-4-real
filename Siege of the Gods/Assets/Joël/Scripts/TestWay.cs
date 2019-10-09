using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWay : EnemyBase
{
    public List<GameObject> waypoint = new List<GameObject>();
    public Transform currentWaypoint;
    public Transform fakeWaypoint;
    public int goal;

    float dis;
    public float rad = 1f;


    public void Start()
    {
        waypoint.AddRange(GameObject.FindGameObjectsWithTag("Goal"));
        waypoint.Add(GameObject.FindGameObjectWithTag("Base"));
        currentWaypoint = waypoint[goal].transform;
    }

    public override void Update()
    {
        base.Update();
        ChangeGoal();
    }

    private void ChangeGoal()
    {
        if (alive)
        {
            if (currentWaypoint != null)
            {
                dis = Vector3.Distance(transform.position, currentWaypoint.position);
                if (dis > rad)
                {
                    movespeed = maxMovespeed;
                    //ResetAnime();
                    //anime.SetTrigger("isWalking");
                    Vector3 direction = currentWaypoint.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = rotation;
                    transform.Translate(Vector3.forward * Time.deltaTime * UIManager.gameSpeed * movespeed);
                }
                else
                {
                    //ResetAnime();
                    //anime.SetTrigger("isIdle");
                    if (fakeWaypoint == null)
                    {
                        CheckForGoal();
                        currentWaypoint = waypoint[goal].transform;
                    }
                }
                CheckifBlocked();
            }
            else
            {
                currentWaypoint = waypoint[goal].transform;
            }
        }
    }

    public void CheckForGoal()
    {
        if(goal < waypoint.Count - 1)
        {
            goal++;
        }
        else
        {
            goal = 0;
        }
    }

    public void CheckifBlocked()
    {
        Vector3 spherepos = transform.localPosition;
        Collider[] collisions = Physics.OverlapSphere(spherepos, rad);
        for (int i = 0; i < collisions.Length; i++)
        {
            if (cooldown <= 0)
            {
                if (collisions[i].tag == "Ally")
                {
                    Attack(collisions[i]);
                    break;
                }
                if (collisions[i].tag == "Base")
                {
                    Attack(collisions[i]);
                    break;
                }
            }
        }
    }

    public void Attack(Collider enemy)
    {
        //ResetAnime();
        //anime.SetTrigger("isAttacking");
        if (enemy.tag == "Ally")
        {
            enemy.GetComponent<AllyAttack>().health -= damage;
        }
        if (enemy.tag == "Base")
        {
            print(gameObject.name);
            enemy.GetComponent<Base>().health -= damage;
            health = 0;
        }
        cooldown = maxCooldown;
    }

    public void ResetAnime()
    {
        anime.ResetTrigger("isWalking");
        anime.ResetTrigger("isDying");
        anime.ResetTrigger("isAttacking");
    }
    private void OnDrawGizmosSelected()
    {
        Vector3 spherepos = transform.localPosition;
        Gizmos.DrawWireSphere(spherepos, rad);
    }
}

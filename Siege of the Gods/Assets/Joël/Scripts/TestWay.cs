
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWay : EnemyBase
{
    public List<GameObject> waypoint = new List<GameObject>();
    public static List<GameObject> staticWay;
    public Transform currentWaypoint;
    public Transform fakeWaypoint;
    public int goal;

    float dis;
    public float rad = 1f;

    void Awake()
    {
        staticWay = waypoint;
        PoisonManager p = GameObject.FindGameObjectWithTag("Manager").GetComponent<PoisonManager>();
        anime = GetComponent<Animator>();
        movespeed = maxMovespeed;
        p.enemies.Add(gameObject);
        waypoint.AddRange(GameObject.FindGameObjectsWithTag("Goal"));
        waypoint.Add(GameObject.FindGameObjectWithTag("Base"));
    }

    public virtual void Start()
    {
        //TODO REMOVE STATIC
        currentWaypoint = staticWay[goal].transform;
    }

    public void Update()
    {
        anime.speed = UIManager.gameSpeed;
        Timer();
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
                    ResetAnime();
                    anime.SetTrigger("isWalking");
                    Vector3 direction = currentWaypoint.position - transform.position;
                    Quaternion rotation = Quaternion.LookRotation(direction);
                    transform.rotation = rotation;
                    transform.Translate(Vector3.forward * Time.deltaTime * UIManager.gameSpeed * movespeed);
                }
                else
                {
                    CheckifBlocked();
                    //ResetAnime();
                    //anime.SetTrigger("isIdle");
                    if (fakeWaypoint == null)
                    {
                        CheckForGoal();
                        currentWaypoint = staticWay[goal].transform;
                    }
                }
            }
            else
            {
                currentWaypoint = staticWay[goal].transform;
            }
        }
    }

    public void CheckForGoal()
    {
        if(goal < staticWay.Count - 1)
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
        if (cooldown <= 0)
        {
            for (int i = 0; i < collisions.Length; i++)
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
        ResetAnime();
        anime.SetTrigger("isAttacking");
        if (enemy.tag == "Ally")
        {
            enemy.GetComponent<AllyAttack>().health -= damage;
        }
        if (enemy.tag == "Base")
        {
            enemy.GetComponent<Base>().health -= damage;
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

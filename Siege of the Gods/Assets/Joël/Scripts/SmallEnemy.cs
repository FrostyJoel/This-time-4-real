using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : TestWay
{
    public override void Start()
    {
        currentWaypoint = waypoint[goal].transform;
    }
}

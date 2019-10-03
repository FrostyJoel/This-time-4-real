using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRange : MonoBehaviour
{
    public GameObject realTowerObject;
    Tower realTower;
    public GameObject range;

    public void Awake()
    {
        realTower = realTowerObject.GetComponentInChildren<Tower>();
        if(range != null && realTower != null)
        {
            range.transform.localScale = new Vector3(realTower.rad * 2, realTower.rad * 2, realTower.rad * 2);
            range.transform.position = transform.position;
        }
    }
}

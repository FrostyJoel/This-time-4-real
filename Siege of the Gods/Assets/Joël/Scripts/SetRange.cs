using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRange : MonoBehaviour
{
    public GameObject realTowerObject;
    public GameObject range;
    Tower realTower;

    public void Awake()
    {
        realTower = realTowerObject.GetComponentInChildren<Tower>();
        if(range != null && realTower != null)
        {
            range.transform.localScale = new Vector3(realTower.rad * 2, range.transform.localScale.y, realTower.rad * 2);
            range.transform.position = transform.position;
        }
    }

    public void TurnOnRange()
    {
        if (!range.activeSelf)
        {
            range.SetActive(true);
        }
        else
        {
            range.SetActive(false);
        }
    }
    
}

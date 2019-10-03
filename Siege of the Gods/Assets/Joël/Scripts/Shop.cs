﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{
    public GameObject currentTower;
    public GameObject tower;
    public GameObject realTower;

    public UIManager ui;

    public bool canBuild;
    public bool towerOnMouse;
    public bool building;

    public int price;

    public float rad;

    public Vector3 currentPos;

    Ray mouse;
    RaycastHit hit;
    private void Awake()
    {
        ui = GetComponent<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        mouse = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouse,out hit,Mathf.Infinity))
        {
            if(!EventSystem.current.IsPointerOverGameObject() && currentTower != null && towerOnMouse == false)
            {
                tower = Instantiate(currentTower, hit.point, Quaternion.identity);
                towerOnMouse = true;
            }
            if (EventSystem.current.IsPointerOverGameObject() && currentTower != null && towerOnMouse)
            {
                Debug.Log("Hey");
                Destroy(tower);
                towerOnMouse = false;
            }
            if (towerOnMouse && currentTower != null)
            {
                currentPos = hit.point;
                tower.gameObject.transform.position = currentPos;
            }
            if(hit.transform.tag == "Tower" && Input.GetButtonDown("Fire1"))
            {
                print("Hey");
            }
        }
        if(tower != null)
        {
            building = true;
            Renderer towerRend = tower.GetComponentInChildren<Renderer>();
            Collider[] col = Physics.OverlapSphere(hit.point,rad);
            int obst = 0;
            for (int i = 0; i < col.Length; i++)
            {
                if(col[i].gameObject != tower)
                {
                    if(col[i].gameObject.tag == "Path" || col[i].gameObject.tag == "Tower" || col[i].gameObject.tag == "Base")
                    {
                        obst++;
                    }
                    if(obst <= 0 && ui.totalMoney > price)
                    {
                        canBuild = true;
                        towerRend.material.color = Color.green;
                    }
                    else
                    {
                        canBuild = false;
                        towerRend.material.color = Color.red;
                    }
                }
            }
            if (Input.GetButtonDown("Fire1") && obst <= 0 && ui.totalMoney >= price)
            {
                Build();
            }
            if (Input.GetButtonDown("Fire2"))
            {
                CancelBuild();
            }
        }
    }

    public void Build()
    {
        ui.totalMoney -= price;
        tower.gameObject.transform.position = currentPos;
        towerOnMouse = false;
        Instantiate(realTower, currentPos, Quaternion.identity);
        CancelBuild();
    }
    public void CancelBuild()
    {
        Destroy(tower);
        currentTower = null;
        building = false;
    }

    public void SetTower(GameObject tower, GameObject real, int cost)
    {
        price = cost;
        currentTower = tower;
        realTower = real;
    }
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hit.point, rad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGrid : MonoBehaviour
{
    public Vector2 GridSize;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrawGizmos()
    {
        Vector3 v = new Vector3(GridSize.x, 1, GridSize.y);
        Gizmos.color = Color.red;
        
    }
}

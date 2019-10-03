using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public int x_pos, z_pos;
    public float tile_cost;
    public float distanceFromStart, distanceToEnd, totalDistance;
    public bool blockade;
    public GridTile previousTile;

    public void Awake()
    {
        tile_cost = Random.Range(0f, 1f);
        if (tile_cost >= 0.85f)
        {
            blockade = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SetColor(new Color(0f, tile_cost, 0f));
    }
    public void CalcuteDistance(float startDist, GridTile target,GridTile previous)
    {
        Vector3 thisTile = new Vector3(x_pos, 0, z_pos);
        // Todo Check of x_pos van deze Tile is of van de vorige
        distanceFromStart = Vector3.Distance(thisTile, new Vector3(previous.x_pos, 0f, previous.z_pos));
        // Todo Check of x_pos van deze Tile is of van de target
        distanceToEnd = Vector3.Distance(thisTile, new Vector3(target.x_pos, 0f, target.z_pos));
        totalDistance = distanceFromStart += distanceToEnd;
    }
    public void SetColor(Color c)
    {
        Renderer r = GetComponent<Renderer>();
        r.material.color = c;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    public List<GridTile> todo = new List<GridTile>();
    public List<GridTile> done = new List<GridTile>();
    GridTile smallestDis;

    // Start is called before the first frame update
    void Start()
    {
        todo = new List<GridTile>();
        done = new List<GridTile>();
    }

    public void StartSearch(GridTile startPos, GridTile endPos)
    {
        todo.Add(startPos);
        smallestDis = startPos;
        while (todo.Count > 0)
        {
            for (int i = 0; i < todo.Count; i++)
            {
                if (smallestDis != null && todo[i].totalDistance <= smallestDis.totalDistance)
                { 
                    smallestDis = todo[i];
                    todo.Remove(todo[i]);
                }
            }

            GridTile[,] grid = GameObject.FindGameObjectWithTag("Manager").GetComponent<GridManager>().grid;
            List<GridTile> neighbour = new List<GridTile>();
            int k, j;
            j = smallestDis.x_pos;
            k = smallestDis.z_pos;

            //Left
            if (j - 1 >= 0 && j != 0)
            {
                if (!grid[j - 1, k].blockade)
                {
                    neighbour.Add(grid[j - 1, k]);
                }
                Debug.Log(grid[j - 1, k].x_pos + " " + grid[j - 1, k].z_pos);
            }
            //Right
            if (j + 1 < grid.GetLength(0) && j != grid.GetLength(0) - 1)
            {
                if (!grid[j + 1, k].blockade)
                {
                    neighbour.Add(grid[j + 1, k]);
                }
                Debug.Log(grid[j + 1, k].x_pos + " "+ grid[j + 1, k].z_pos);
            }
            //Below
            if (k - 1 >= 0 && k != 0)
            {
                if (!grid[j, k - 1].blockade)
                {
                    neighbour.Add(grid[j, k - 1]);
                }
                Debug.Log(grid[j, k - 1].x_pos +" "+ grid[j, k - 1].z_pos);
            }
            //Above
            if (k != grid.GetLength(1) - 1)
            {
                if (!grid[j, k + 1].blockade)
                { 
                    neighbour.Add(grid[j, k + 1]);
                }
                Debug.Log(grid[j, k + 1].x_pos +" " + grid[j, k + 1].z_pos);
            }
            //Left + Below
            if (j - 1 >= 0 && k - 1 >= 0 && j != 0 && k != 0)
            {
                if(!grid[j - 1, k - 1].blockade)
                {
                    neighbour.Add(grid[j - 1, k - 1]);
                }
                Debug.Log(grid[j - 1, k - 1].x_pos +" "+ grid[j - 1, k - 1].z_pos);
            }
            //Left + Above
            if (j - 1 >= 0 && j != 0 && k != grid.GetLength(1) - 1)
            {
                if(!grid[j - 1, k + 1].blockade)
                {
                    neighbour.Add(grid[j - 1, k + 1]);
                }
                Debug.Log(grid[j - 1, k + 1].x_pos +" "+ grid[j - 1, k + 1].z_pos);
            }
            //Right + Below
            if (j + 1 >= 0 && k - 1 >= 0 && j != grid.GetLength(0) - 1 && k != 0)
            {
                if(!grid[j + 1, k - 1].blockade)
                {
                    neighbour.Add(grid[j + 1, k - 1]);
                }
                Debug.Log(grid[j + 1, k - 1].x_pos + " " + grid[j + 1, k - 1].z_pos);
            }
            //Right + Above
            if (j + 1 >= 0 && j != grid.GetLength(0) - 1 && k != grid.GetLength(1) - 1)
            {
                if(!grid[j + 1, k + 1].blockade)
                {
                    neighbour.Add(grid[j + 1, k + 1]);
                }
                Debug.Log(grid[j + 1, k + 1].x_pos +" "+ grid[j + 1, k + 1].z_pos);
            }

            Debug.Log("Neighbours Total: " + neighbour.Count);
            Debug.Log("Smallest dis: " + smallestDis.x_pos + " " + smallestDis.z_pos);

            foreach (GridTile neigbouringGrid in neighbour)
            {
                float distanceFromStart = startPos.distanceFromStart + neigbouringGrid.distanceFromStart;
                float totalDistance = startPos.distanceFromStart + startPos.distanceToEnd;
                if (neigbouringGrid == endPos)
                {
                    neigbouringGrid.CalcuteDistance(distanceFromStart, neigbouringGrid, startPos);
                }
                else if (done.Contains(neigbouringGrid))
                {
                    neigbouringGrid.SetColor(Color.red);
                    break;
                }
                else if (todo.Contains(neigbouringGrid)) 
                {
                    neigbouringGrid.SetColor(Color.blue);
                    break;
                }
                else
                {
                    todo.Add(neigbouringGrid);
                    neigbouringGrid.CalcuteDistance(distanceFromStart, endPos, smallestDis);
                    done.Add(neigbouringGrid);
                    todo.Remove(neigbouringGrid);
                }
            }
        }
        if (todo.Count <= 0)
        {
            Debug.LogError("No Target");
        }
    }
}

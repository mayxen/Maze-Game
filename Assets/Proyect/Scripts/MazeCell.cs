using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

public class MazeCell : MonoBehaviour
{
    public IntVector2 Coordinates;
    public MazeRoom Room;
    MazeCellEdge[] edges = new MazeCellEdge[MazeDirecctions.Count];
    int initializedEdgeCount;

    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirecctions.Count;
        }
     }
    public MazeDirecction RandomUnitializedDirection
    {
        get
        {
            int skips = random.Range(0, MazeDirecctions.Count - initializedEdgeCount);

            for (int i = 0; i < MazeDirecctions.Count; i++)
            {
                if(edges[i] == null) {
                    if (skips == 0)
                        return (MazeDirecction)i;

                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("Mazecell has no unitialized direction left");
        }
    }


    public void Initialized(MazeRoom room)
    {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.Settings.FloorMaterial;
    }
    public MazeCellEdge GetEdge(MazeDirecction direction)
    {
        return edges[(int)direction];
    }

    public void SetEdge(MazeDirecction direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

}

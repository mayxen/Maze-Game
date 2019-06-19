using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeWall : MazeCellEdge
{

    public override void Inicialize(MazeCell cell, MazeCell otherCell, MazeDirecction direction)
    {
        base.Inicialize(cell, otherCell, direction);
        transform.GetChild(0).GetComponent<Renderer>().material = cell.Room.Settings.wallMaterial;
    }
}

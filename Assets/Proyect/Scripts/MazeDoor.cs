using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDoor : MazePassage
{
    public Transform Hinge;
    MazeDoor otherSideofDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
        }
    }

    public override void Inicialize(MazeCell cell, MazeCell otherCell, MazeDirecction direction)
    {
        base.Inicialize(cell, otherCell, direction);
        if(otherSideofDoor != null)
        {
            #pragma warning disable 0219
            Hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = Hinge.localPosition;
            p.x = -p.x;
            Hinge.localPosition = p;
            #pragma warning restore 0219
        }
    }
}

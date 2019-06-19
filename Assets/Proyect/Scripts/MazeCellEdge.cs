
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour
{
    public MazeCell cell;
    public MazeCell otherCell;
    public MazeDirecction direction;

    public virtual void Inicialize(MazeCell cell, MazeCell otherCell, MazeDirecction direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;

        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}

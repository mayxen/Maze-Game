using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    MazeCell currentCell;

    public void SetLocation(MazeCell cell)
    {
        currentCell = cell;
        transform.localPosition = new Vector3(cell.transform.localPosition.x,0.5f, cell.transform.localPosition.z);
    }

    void Move(MazeDirecction direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);

        if (edge is MazePassage)
            SetLocation(edge.otherCell);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

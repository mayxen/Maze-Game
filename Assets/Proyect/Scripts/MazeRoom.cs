using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : ScriptableObject
{
    public int SettingsIndex;
    public MazeRoomSettings Settings;

    private List<MazeCell> cells = new List<MazeCell>();

    public void Add(MazeCell cell)
    {
        cell.Room = this;
        cells.Add(cell);
    }

    public void Assimilate(MazeRoom room)
    {
        for (int i = 0; i < room.cells.Count; i++)
        {
            Add(room.cells[i]);
        }
    }
}

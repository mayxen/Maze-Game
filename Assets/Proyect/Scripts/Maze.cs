using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Maze : MonoBehaviour
{
    public IntVector2 Size;
    public MazeCell CellPrefab;
    public float GenerationStepDelay;
    public MazePassage PassagePrefab;
    public MazeWall[] WallPrefabs;
    public MazeDoor DoorPrefab;
    public MazeRoomSettings[] RoomSettings;

    [Range(0f, 1f)]
    public float DoorProbability;

    MazeCell[,] cells;
    List<MazeRoom> rooms = new List<MazeRoom>();
    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(Random.Range(0,Size.X), Random.Range(0, Size.Z));
        }
    }

    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.X, coordinates.Z];
    }

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(GenerationStepDelay);
        cells = new MazeCell[Size.X, Size.Z];

        List<MazeCell> activeCells = new List<MazeCell>();
        
        DoFirstGenerationStep(activeCells);

        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }

        //for (int x = 0; x < Size.X; x++)
        //{
        //    for (int z = 0; z < Size.Z; z++)
        //    {
        //        yield return delay;
        //        CreateCell(new IntVector2(x, z));
        //    }
        //}
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newcell = CreateCell(RandomCoordinates);
        newcell.Initialized(CreateRoom(-1));
        activeCells.Add(newcell);
    }

    private void CreatePassageInSameRoom(MazeCell cell, MazeCell other, MazeDirecction direction)
    {
        MazePassage passage = Instantiate(PassagePrefab);
        passage.Inicialize(cell, other, direction);
        passage = Instantiate(PassagePrefab);
        passage.Inicialize(other, cell, direction.GetOpposite());

        if(cell.Room != other.Room)
        {
            MazeRoom roomToAssimilate = other.Room;
            cell.Room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count -1;
        MazeCell currentCell = activeCells[currentIndex];

        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirecction direcction = currentCell.RandomUnitializedDirection;
        IntVector2 coordinates = currentCell.Coordinates + direcction.ToIntVector2();

        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbour = GetCell(coordinates);
            if(neighbour == null)
            {
                neighbour = CreateCell(coordinates);
                CreatePassage(currentCell,neighbour,direcction);
                activeCells.Add(neighbour);
            }
            else if(currentCell.Room.SettingsIndex == neighbour.Room.SettingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbour, direcction);
            }
            else
            {
                CreateWall(currentCell,neighbour,direcction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direcction);
        }
            
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirecction direcction)
    {
        MazeWall wall = Instantiate(WallPrefabs[Random.Range(0,WallPrefabs.Length)]);
        wall.Inicialize(cell, otherCell, direcction);

        if(otherCell != null)
        {
            wall = Instantiate(WallPrefabs[Random.Range(0, WallPrefabs.Length)]);
            wall.Inicialize(otherCell, cell, direcction.GetOpposite());
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirecction direcction)
    {
        MazePassage prefab = Random.value < DoorProbability ? DoorPrefab : PassagePrefab;
        MazePassage passage = Instantiate(prefab);
        passage.Inicialize(cell, otherCell, direcction);
        passage = Instantiate(prefab);
        if(passage is MazeDoor)
        {
            otherCell.Initialized(CreateRoom(cell.Room.SettingsIndex));
        }
        else
        {
            otherCell.Initialized(cell.Room);
        }
        passage.Inicialize(otherCell,cell, direcction.GetOpposite());
    }

    

    private bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < Size.X && coordinate.Z >= 0 && coordinate.Z < Size.Z;
    }

    private MazeCell CreateCell(IntVector2 coor)
    {
        MazeCell newCell = Instantiate(CellPrefab);
        cells[coor.X, coor.Z] = newCell;
        newCell.Coordinates = coor;
        newCell.name = "Maze cell " + coor.X + " " + coor.Z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coor.X - Size.X * 0.5f + 0.5f, 0f, coor.Z - Size.Z * 0.5f + 0.5f);
        return newCell;
    }

    private MazeRoom CreateRoom(int indexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.SettingsIndex = Random.Range(0,RoomSettings.Length);

        if(newRoom.SettingsIndex == indexToExclude)
        {
            newRoom.SettingsIndex = (newRoom.SettingsIndex + 1) % RoomSettings.Length;
        }

        newRoom.Settings = RoomSettings[newRoom.SettingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }
}

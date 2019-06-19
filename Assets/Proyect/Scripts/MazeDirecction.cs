using UnityEngine;

public enum MazeDirecction
{
    North,
    East,
    South,
    West
}

public static class MazeDirecctions
{
    public const int Count = 4;

    public static MazeDirecction RandomValue
    {
        get
        {
            return (MazeDirecction)Random.Range(0, Count);
        }
    }

    private static IntVector2[] Vectors =
    {
        new IntVector2(0,1),
        new IntVector2(1,0),
        new IntVector2(0,-1),
        new IntVector2(-1,0),
    };

    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270f,0f)
    };

    private static MazeDirecction[] opposites =
    {
        MazeDirecction.South,
        MazeDirecction.West,
        MazeDirecction.North,
        MazeDirecction.East
    };

    public static MazeDirecction GetOpposite(this MazeDirecction direcction)
    {
        return opposites[(int)direcction];
    }

    public static IntVector2 ToIntVector2(this MazeDirecction direcction)
    {
        return Vectors[(int)direcction];
    }

    public static Quaternion ToRotation(this MazeDirecction direcction)
    {
        return rotations[(int)direcction];
    }

}

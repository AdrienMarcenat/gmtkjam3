using UnityEngine;

public struct TileCoordinates
{
    public TileCoordinates(int x = 0, int y = 0)
    {
        this.x = x;
        this.y = y;
    }

    public static TileCoordinates operator +(TileCoordinates t1, TileCoordinates t2)
    {
        TileCoordinates res = new TileCoordinates();
        res.x = t1.x + t2.x;
        res.y = t1.y + t2.y;
        return res;
    }

    public static bool operator ==(TileCoordinates t1, TileCoordinates t2)
    {
        return t1.x == t2.x && t1.y == t2.y;
    }

    public static bool operator !=(TileCoordinates t1, TileCoordinates t2)
    {
        return t1.x != t2.x || t1.y != t2.y;
    }

    public static implicit operator TileCoordinates(Vector3 vector)
    {
        TileCoordinates res = new TileCoordinates();
        res.x = (int)vector.x;
        res.y = (int)vector.y;
        return res;
    }

    public int x;
    public int y;
}

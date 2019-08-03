using UnityEngine;

public struct TileCoordinates
{
    public static int ms_Modulo = 15;

    public TileCoordinates(int x = 0, int y = 0)
    {
        this.x = x.Modulo(ms_Modulo);
        this.y = y.Modulo(ms_Modulo);
    }

    public static TileCoordinates operator +(TileCoordinates t1, TileCoordinates t2)
    {
        TileCoordinates res = new TileCoordinates();
        res.x = (t1.x + t2.x).Modulo(ms_Modulo);
        res.y = (t1.y + t2.y).Modulo(ms_Modulo);
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
        res.x = ((int)vector.x).Modulo(ms_Modulo);
        res.y = ((int)vector.y).Modulo(ms_Modulo);
        return res;
    }

    public int x;
    public int y;
}

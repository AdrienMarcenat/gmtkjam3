using System.Collections.Generic;
using UnityEngine;

public enum ETileType
{
    Invalid,
    None,
    Goal,
    Wall,
}

public enum EDirection
{
    Right,
    Up,
    Left,
    Down
}

public class Tile : MonoBehaviour
{
    [SerializeField] private ETileType m_Type;
    [SerializeField] private TileObject m_Object;
    private TileCoordinates m_Coordinates;

    public bool IsEmpty ()
    {
        return m_Object == null || !IsObstacle ();
    }

    public virtual bool IsObstacle() { return false; }

    public TileCoordinates GetCoordinates ()
    {
        return m_Coordinates;
    }

    public void SetCoordinates (TileCoordinates coordinates)
    {
        m_Coordinates = coordinates;
    }

    ETileType GetTileType()
    {
        return m_Type;
    }

    public void SetTileType (ETileType type)
    {
        m_Type = type;
    }

    public TileObject GetTileObject()
    {
        return m_Object;
    }

    public void SetTileObject (TileObject tileObject)
    {
        m_Object = tileObject;
        if (m_Object != null)
        {
            m_Object.SetCoordinates (m_Coordinates);
        }
    }

    public virtual void Evaluate()
    { }

    private static Dictionary<EDirection, TileCoordinates> ms_NeighboorTiles = new Dictionary<EDirection, TileCoordinates>()
    {
        { EDirection.Right, new TileCoordinates(1, 0) },
        { EDirection.Left,  new TileCoordinates(-1, 0) },
        { EDirection.Up,    new TileCoordinates(0, 1) },
        { EDirection.Down,  new TileCoordinates(0, -1) },
    };
}

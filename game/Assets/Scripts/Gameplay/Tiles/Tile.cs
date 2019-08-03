using UnityEngine;

public enum ETileType
{
    Invalid,
    None,
    Normal,
}

public class Tile : MonoBehaviour
{
    [SerializeField] private ETileType m_Type;
    [SerializeField] private TileObject m_Object;
    private TileCoordinates m_Coordinates;

    public bool IsEmpty ()
    {
        return m_Object == null || !m_Object.IsObstacle ();
    }

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
}

using UnityEngine;

public enum ETileObjectType
{
    None,
    Player,
    Cube,
    Goal,
    Wall,
}

public class TileObject : MonoBehaviour
{
    [SerializeField] private ETileObjectType m_Type;
    private TileCoordinates m_Coordinates;

    public virtual void Init (ETileObjectType type, int x, int y, string[] args)
    {
        m_Type = type;
        m_Coordinates = new TileCoordinates (x, y);
    }

    public ETileObjectType GetObjectType ()
    {
        return m_Type;
    }

    public void SetObjectType (ETileObjectType type)
    {
        m_Type = type;
    }

    public TileCoordinates GetCoordinates ()
    {
        return m_Coordinates;
    }

    public void SetCoordinates (TileCoordinates coordinates)
    {
        m_Coordinates = coordinates;
    }

    public virtual bool IsObstacle ()
    {
        return false;
    }
}

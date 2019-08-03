
using System.Collections.Generic;

public class TileManager
{
    private Dictionary<TileCoordinates, Tile> m_Tiles;
    private Dictionary<TileCoordinates, TileObject> m_TileObjects;

    public TileManager ()
    {
        m_Tiles = new Dictionary<TileCoordinates, Tile>();
        m_TileObjects = new Dictionary<TileCoordinates, TileObject> ();
    }

    public Tile GetTile (int x, int y)
    {
        return GetTile (new TileCoordinates (x, y));
    }

    public Tile GetTile (TileCoordinates coordinates)
    {
        Tile tile = null;
        m_Tiles.TryGetValue (coordinates, out tile);
        return tile;
    }

    public void AddTile (Tile tile)
    {
        m_Tiles.Add (tile.GetCoordinates (), tile);
    }
    public void AddTileObject(TileObject tileObject)
    {
        m_TileObjects.Add(tileObject.GetCoordinates(), tileObject);
    }

    public void RemoveTileObject(TileObject tileObject)
    {
        m_TileObjects.Remove(tileObject.GetCoordinates());
    }

    public void UpdateTileObjects()
    {
        m_TileObjects.Clear();
        new UpdateTileObjectsEvent().Push();
    }

    public TileObject GetObjectInTile(TileCoordinates coordinates)
    {
        TileObject tileObject = null;
        m_TileObjects.TryGetValue(coordinates, out tileObject);
        return tileObject;
    }

    public void Reset ()
    {
        m_Tiles.Clear ();
        m_TileObjects.Clear();
    }
}

public class TileManagerProxy : UniqueProxy<TileManager>
{ }
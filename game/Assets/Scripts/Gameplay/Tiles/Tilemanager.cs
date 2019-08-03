
using System.Collections.Generic;

public class TileManager
{
    private Dictionary<TileCoordinates, Tile> m_Tiles;

    public TileManager ()
    {
        m_Tiles = new Dictionary<TileCoordinates, Tile> ();
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
        this.m_Tiles.Add (tile.GetCoordinates (), tile);
    }

    public void SetTileObject(TileCoordinates coordinates, TileObject tileObject)
    {
        Tile tile = null;
        m_Tiles.TryGetValue (coordinates, out tile);
        if(tile != null)
        {
            tile.SetTileObject (tileObject);
        }
    }

    public void Reset ()
    {
        m_Tiles.Clear ();
    }
}

public class TileManagerProxy : UniqueProxy<TileManager>
{ }
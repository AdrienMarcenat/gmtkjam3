
using System;
using System.IO;
using UnityEngine;

public static class LevelParser
{
    public static int GenLevel (string filename)
    {
        filename = Application.streamingAssetsPath + filename;

        string[] lines = File.ReadAllLines (filename);
        string[] firstLineWords = lines[0].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int levelSize = firstLineWords.Length;
        TileCoordinates.ms_Modulo = levelSize;
        int middleY = levelSize / 2;

        // First line is north wall description
        GenerateTiles(lines[0], middleY + 1, false);

        // Second line is tile description
        GenerateTiles(lines[1], middleY, false);

        // Third line is tileObject description
        string[] lineOfTileObject = lines[2].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int x = 0;
        foreach (string tileInfo in lineOfTileObject)
        {
            string[] words = tileInfo.Split(',');

            ETileObjectType tileObjectType = (ETileObjectType)Enum.Parse (typeof (ETileObjectType), (String)words[0], true);
            if(tileObjectType == ETileObjectType.None)
            {
                x++;
                continue;
            }
            GameObject tileObjectGameObject = GameObject.Instantiate (RessourceManager.LoadPrefab ("TileObject_" + words[0]));
            tileObjectGameObject.transform.position = new Vector3 (x.ToWorldUnit (), middleY.ToWorldUnit (), 0);
            TileObject tileObject = tileObjectGameObject.GetComponent<TileObject> ();
            tileObject.Init (tileObjectType, x, middleY, words.SubArray (1, -1));

            x++;
        }

        // Second line is south wall description
        GenerateTiles(lines[3], middleY - 1, true);

        // We want a square world
        return levelSize;
    }

    private static void GenerateTiles(string tileDescription, int y, bool flipY)
    {
        string[] lineOfTile = tileDescription.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        int x = 0;
        foreach (string tileInfo in lineOfTile)
        {
            string[] words = tileInfo.Split(',');

            ETileType tileType = (ETileType)Enum.Parse(typeof(ETileType), (String)words[0], true);
            GameObject tileGameObject = GameObject.Instantiate(RessourceManager.LoadPrefab("Tile_" + words[0]));
            tileGameObject.transform.position = new Vector3(x.ToWorldUnit(), y.ToWorldUnit(), 0);
            Tile tile = tileGameObject.GetComponent<Tile>();
            tile.Init(tileType, x, y, words.SubArray(1, -1));
            if (flipY)
            {
                tileGameObject.GetComponent<SpriteRenderer>().flipY = true;
            }

            x++;
        }
    }
}

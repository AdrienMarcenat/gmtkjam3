using UnityEngine;
using System.Collections.Generic;

public class CubeDestroyer : Tile
{
    [SerializeField] private AudioClip m_DestroySound;
    private Stack<GameObject> m_DestroyedCubes = new Stack<GameObject>();

    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        return tileObject != null && tileObject.IsCube();
    }

    public override void DoRule()
    {
        TileObject tileObject = GetTileObject();
        TileManagerProxy.Get().RemoveTileObject(tileObject);
        GameObject cube = tileObject.gameObject;
        cube.SetActive(false);
        m_DestroyedCubes.Push(cube);
        GameObject flash = Instantiate(RessourceManager.LoadPrefab("Flash"));
        flash.transform.position = transform.position;
        SoundManagerProxy.Get().PlayMultiple(m_DestroySound);
    }

    public override void UndoRule()
    {
        GameObject cube = m_DestroyedCubes.Pop();
        cube.SetActive(true);
        TileObject tileObject = cube.GetComponent<TileObject>();
        TileManagerProxy.Get().AddTileObject(tileObject);
    }
}

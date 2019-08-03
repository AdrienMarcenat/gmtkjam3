﻿
using UnityEngine;

public class Cube : TileObject
{
    public override void Init (ETileObjectType type, int x, int y, string[] args)
    {
        base.Init (type, x, y, args);
    }

    public override bool IsObstacle ()
    {
        return true;
    }
}
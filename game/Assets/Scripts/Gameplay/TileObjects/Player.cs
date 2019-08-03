using UnityEngine;

public class Player : TileObject
{
    public override void Init(ETileObjectType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
    }

    public override bool IsObstacle ()
    {
        return false;
    }
}

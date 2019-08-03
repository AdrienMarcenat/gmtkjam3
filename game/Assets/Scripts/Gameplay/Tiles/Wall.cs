using UnityEngine;

public class Wall : Tile
{
    public override bool IsObstacle() { return true; }

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 0)
        {
            GetComponent<SpriteRenderer>().sprite = RessourceManager.LoadSprite(args[0], 0);
        }
    }
}

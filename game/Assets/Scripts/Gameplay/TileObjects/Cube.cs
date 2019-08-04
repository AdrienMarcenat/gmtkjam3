
using UnityEngine;

public class Cube : MovingTileObject
{
    public override void Init(ETileObjectType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 0)
        {
            int isGoldCube = int.Parse(args[0]);
            if(isGoldCube == 1)
            {
                gameObject.tag = "GoldCube";
                GetComponent<SpriteRenderer>().color = Color.yellow;
                new GoldCubeCreation().Push();
            }
        }
    }
}

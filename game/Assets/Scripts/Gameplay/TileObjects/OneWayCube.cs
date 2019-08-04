
using System;
using UnityEngine;

public class OneWayCube : Cube
{
    private EDirection m_AllowedDirection;
    private TileCoordinates m_AllowedTileVector;

    public override void Init(ETileObjectType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 1)
        {
            m_AllowedDirection = (EDirection)Enum.Parse(typeof(EDirection), (String)args[1], true);
            m_AllowedTileVector = Tile.ms_DirectionTileVector[m_AllowedDirection];
            if(m_AllowedDirection == EDirection.Left)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    public override bool EvaluateMoveTo(int xDir, int yDir)
    {
        TileCoordinates tileVector = new TileCoordinates(xDir, yDir);
        return m_AllowedTileVector == tileVector && base.EvaluateMoveTo(xDir, yDir);
    }
}

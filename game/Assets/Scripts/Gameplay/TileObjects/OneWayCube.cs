
using UnityEngine;

public class OneWayCube : TwinCube
{
    [SerializeField] private EDirection m_AllowedDirection;
    private TileCoordinates m_AllowedTileVector;

    public new void Awake()
    {
        base.Awake();
        m_AllowedTileVector = Tile.ms_DirectionTileVector[m_AllowedDirection];
    }

    public override bool EvaluateMoveTo(int xDir, int yDir)
    {
        TileCoordinates tileVector = new TileCoordinates(xDir, yDir);
        return m_AllowedTileVector == tileVector && base.EvaluateMoveTo(xDir, yDir);
    }
}

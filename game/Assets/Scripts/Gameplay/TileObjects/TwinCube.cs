
using UnityEngine;

public class TwinCube : Cube
{
    private TwinCube m_TwinCube;
    private TileCoordinates m_TwinCubeCoordinate;
    private bool m_RecursionGuard = false;

    public override void Init(ETileObjectType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 1)
        {
            int twinX = int.Parse(args[1]);
            m_TwinCubeCoordinate = new TileCoordinates(twinX, y);
        }
    }

    protected TwinCube GetTwinCube()
    {
        if (m_TwinCube == null)
        {
            TileObject twin = TileManagerProxy.Get().GetObjectInTile(m_TwinCubeCoordinate);
            m_TwinCube = (TwinCube)twin;
        }
        return m_TwinCube;
    }

    public override void AddMoveCommand(int xDir, int yDir)
    {
        if (!m_RecursionGuard)
        {
            m_RecursionGuard = true;
            base.AddMoveCommand(xDir, yDir);
            TwinCube twin = GetTwinCube();
            if (twin != null)
            {
                twin.TryMove(xDir, yDir);
            }
            m_RecursionGuard = false;
        }
    }
}

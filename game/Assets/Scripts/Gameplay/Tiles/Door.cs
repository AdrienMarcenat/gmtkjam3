using UnityEngine;

public class Door : Tile
{
    private bool m_IsOpen = false;

    public override bool IsObstacle() { return !m_IsOpen; }

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
    }

    public void Open()
    {
        m_IsOpen = true;
    }

    public void Close()
    {
        m_IsOpen = false;
    }
}

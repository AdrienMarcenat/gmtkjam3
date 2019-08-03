
using UnityEngine;

public class TwinCube : Cube
{
    [SerializeField] private TwinCube m_TwinCube;
    private bool m_RecursionGuard = false;

    public override void AddMoveCommand(int xDir, int yDir)
    {
        if (!m_RecursionGuard)
        {
            m_RecursionGuard = true;
            base.AddMoveCommand(xDir, yDir);
            if (m_TwinCube != null)
            {
                m_TwinCube.TryMove(xDir, yDir);
            }
            m_RecursionGuard = false;
        }
    }
}

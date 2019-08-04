using UnityEngine;

public class Door : Tile
{
    private bool m_IsOpen = false;
    private Animator m_Animator;

    public override bool IsObstacle() { return !m_IsOpen; }

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        m_Animator = GetComponent<Animator>();
    }

    public void Open()
    {
        m_IsOpen = true;
        m_Animator.SetTrigger("Open");
    }

    public void Close()
    {
        m_Animator.SetTrigger("Close");
        m_IsOpen = false;
    }
}


using UnityEngine;

public class Cube : MovingTileObject
{
    [SerializeField] private int m_X = 0;
    [SerializeField] private int m_Y = 0;

    public new void Awake()
    {
        base.Awake();
        Init(ETileObjectType.Cube, m_X, m_Y, null);
    }

    public override void Init (ETileObjectType type, int x, int y, string[] args)
    {
        base.Init (type, x, y, args);
    }
}

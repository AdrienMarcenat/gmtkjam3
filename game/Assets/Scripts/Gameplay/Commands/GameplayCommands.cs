using UnityEngine;

public class NullCommand : Command
{
    public NullCommand(GameObject actor) : base(actor)
    {
    }

    public override void Execute()
    {
    }

    public override void Undo()
    {
    }
}

public class MoveCommand : Command
{
    public override void Execute()
    {
        MovingTileObject movingTileObject = m_Actor.GetComponent<MovingTileObject>();
        movingTileObject.Move(m_XDir, m_YDir);
    }

    public override void Undo()
    {
        MovingTileObject movingTileObject = m_Actor.GetComponent<MovingTileObject>();
        movingTileObject.MoveInstant(-m_XDir, -m_YDir);
    }

    public MoveCommand(GameObject actor, int xDir, int yDir) : base(actor)
    {
        m_XDir = xDir;
        m_YDir = yDir;
    }

    private int m_XDir;
    private int m_YDir;
}
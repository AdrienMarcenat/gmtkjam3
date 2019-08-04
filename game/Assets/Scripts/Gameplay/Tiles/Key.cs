using UnityEngine;

public class Key : Tile
{
    private Door m_Door;
    private TileCoordinates m_DoorCoordinate;
    private bool m_HasOpen = false;
    private Animator m_Animator;

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 0)
        {
            int doorX = int.Parse(args[0]);
            m_DoorCoordinate = new TileCoordinates(doorX, y);
        }
        m_Animator = GetComponent<Animator>();
    }

    protected Door GetDoor()
    {
        if (m_Door == null)
        {
            Tile door = TileManagerProxy.Get().GetTile(m_DoorCoordinate);
            m_Door = (Door)door;
        }
        return m_Door;
    }

    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        return !m_HasOpen && tileObject != null && tileObject.GetObjectType() == ETileObjectType.Player;
    }

    public override void DoRule()
    {
        GetDoor().Open();
        m_Animator.SetTrigger("Open");
        m_HasOpen = true;
    }

    public override void UndoRule()
    {
        m_HasOpen = false;
        m_Animator.SetTrigger("Close");
        GetDoor().Close();
    }
}

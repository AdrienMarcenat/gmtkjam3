using System.Collections.Generic;
using UnityEngine;

public enum ETileType
{
    Invalid,
    None,
    Goal,
    Wall,
}

public enum EDirection
{
    Right,
    Up,
    Left,
    Down
}

public class Tile : MonoBehaviour
{
    [SerializeField] private ETileType m_Type;
    private TileCoordinates m_Coordinates;
    private CommandStack m_CommandStack = new CommandStack();

    private int m_X;
    private int m_Y;

    public void Awake()
    {
        m_X = (int)transform.position.x;
        m_Y = (int)transform.position.y;
        SetCoordinates(new TileCoordinates(m_X, m_Y));
        TileManagerProxy.Get().AddTile(this);
        this.RegisterAsListener("Game", typeof(UndoTileEvent), typeof(EvaluateRuleEvent));
    }

    public void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(UndoTileEvent undoEvent)
    {
        m_CommandStack.Undo();
    }

    public void OnGameEvent(EvaluateRuleEvent evaluateRuleEvent)
    {
        RuleCommand command = new RuleCommand(gameObject);
        command.Execute();
        PushCommand(command);
    }

    public virtual bool IsObstacle() { return false; }

    public TileCoordinates GetCoordinates ()
    {
        return m_Coordinates;
    }

    public void SetCoordinates (TileCoordinates coordinates)
    {
        m_Coordinates = coordinates;
    }

    ETileType GetTileType()
    {
        return m_Type;
    }

    public void SetTileType (ETileType type)
    {
        m_Type = type;
    }

    public TileObject GetTileObject()
    {
        return TileManagerProxy.Get().GetObjectInTile(GetCoordinates());
    }

    public virtual void EvaluateRule()
    {
    }

    public virtual void UndoRule()
    {
    }

    private void PushCommand(Command command)
    {
        m_CommandStack.PushCommand(command);
    }

    private static Dictionary<EDirection, TileCoordinates> ms_NeighboorTiles = new Dictionary<EDirection, TileCoordinates>()
    {
        { EDirection.Right, new TileCoordinates(1, 0) },
        { EDirection.Left,  new TileCoordinates(-1, 0) },
        { EDirection.Up,    new TileCoordinates(0, 1) },
        { EDirection.Down,  new TileCoordinates(0, -1) },
    };
}

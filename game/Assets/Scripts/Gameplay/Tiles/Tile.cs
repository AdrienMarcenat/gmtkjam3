using System.Collections.Generic;
using UnityEngine;

public enum ETileType
{
    Invalid,
    Normal,
    Goal,
    Wall,
    Emitter,
    Receiver,
    CubeDestroyer,
    Door,
    Key,
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
    private bool m_ShoudlDoRule = false;

    public virtual void Init(ETileType type, int x, int y, string[] args)
    {
        m_Type = type;
        m_CommandStack = new CommandStack();
        m_Coordinates = new TileCoordinates(x, y);
        TileManagerProxy.Get().AddTile(this);
    }

    public void Awake()
    {
        this.RegisterAsListener("Game", typeof(UndoTileEvent), typeof(EvaluateRuleEvent), typeof(DoRuleEvent));
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
        m_ShoudlDoRule = EvaluateRule();
    }

    public void OnGameEvent(DoRuleEvent doRuleEvent)
    {
        Command command = null;
        if (m_ShoudlDoRule)
        {
            command = new RuleCommand(gameObject);
        }
        else
        {
            command = new NullCommand(gameObject);
        }
        m_ShoudlDoRule = false;
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

    public virtual bool EvaluateRule()
    {
        return false;
    }

    public virtual void DoRule()
    {
    }

    public virtual void UndoRule()
    {
    }

    private void PushCommand(Command command)
    {
        m_CommandStack.PushCommand(command);
    }

    public static Dictionary<EDirection, Vector2Int> ms_DirectionTileVector = new Dictionary<EDirection, Vector2Int>()
    {
        { EDirection.Right, new Vector2Int(1, 0) },
        { EDirection.Left,  new Vector2Int(-1, 0) },
        { EDirection.Up,    new Vector2Int(0, 1) },
        { EDirection.Down,  new Vector2Int(0, -1) },
    };
}

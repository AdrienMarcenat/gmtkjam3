using UnityEngine;

public enum ETileObjectType
{
    None,
    Player,
    Cube,
    Goal,
    Wall,
}

public class TileObject : MonoBehaviour
{
    [SerializeField] private ETileObjectType m_Type;
    private TileCoordinates m_Coordinates;
    private CommandStack m_CommandStack = new CommandStack();
    protected bool m_HasAlreadyAddedCommand = false;

    public void Awake()
    {
        this.RegisterAsListener("Game", typeof(UndoTileObjectEvent), typeof(MoveEvent), typeof(UpdateTileObjectsEvent));
    }

    public void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(UndoTileObjectEvent undoEvent)
    {
        m_CommandStack.Undo();
    }

    public void OnGameEvent(MoveEvent moveEvent)
    {
        if(!m_HasAlreadyAddedCommand)
        {
            NullCommand command = new NullCommand(gameObject);
            command.Execute();
            PushCommand(command);
        }
        m_HasAlreadyAddedCommand = false;
    }

    public void OnGameEvent(UpdateTileObjectsEvent updateTileObjectsEvent)
    {
        TileManagerProxy.Get().AddTileObject(this);
    }

    public virtual void Init(ETileObjectType type, int x, int y, string[] args)
    {
        m_Type = type;
        m_CommandStack = new CommandStack();
        m_Coordinates = new TileCoordinates(x, y);
        TileManagerProxy.Get().AddTileObject(this);
    }

    public ETileObjectType GetObjectType()
    {
        return m_Type;
    }

    public void SetObjectType(ETileObjectType type)
    {
        m_Type = type;
    }

    public TileCoordinates GetCoordinates()
    {
        return m_Coordinates;
    }

    public void SetCoordinates(TileCoordinates coordinates)
    {
        m_Coordinates = coordinates;
    }

    public virtual bool EvaluateMoveTo(int xDir, int yDir)
    {
        return false;
    }

    public virtual void AddMoveCommand(int xDir, int yDir)
    { }

    protected void PushCommand(Command command)
    {
        m_CommandStack.PushCommand(command);
    }
}

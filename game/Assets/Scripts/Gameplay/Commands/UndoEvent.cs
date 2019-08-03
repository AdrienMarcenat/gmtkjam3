
public class UndoEvent : GameEvent
{
    public UndoEvent() : base("Game", EProtocol.Instant)
    {
    }
}

public class UndoTileObjectEvent : UndoEvent
{
}

public class UndoTileEvent : UndoEvent
{
}
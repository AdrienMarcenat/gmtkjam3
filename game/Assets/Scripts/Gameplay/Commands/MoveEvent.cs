
public class MoveEvent : GameEvent
{
    public MoveEvent() : base("Game", EProtocol.Instant)
    {
    }
}

public class UpdateTileObjectEvent : GameEvent
{
    public UpdateTileObjectEvent() : base("Game", EProtocol.Instant)
    {
    }
}


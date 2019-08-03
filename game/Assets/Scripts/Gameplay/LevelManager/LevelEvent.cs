
public class LevelEvent : GameEvent
{
    public LevelEvent(int levelIndex, bool enter) : base("Game", EProtocol.Instant)
    {
        m_LevelIndex = levelIndex;
        m_Enter = enter;
    }

    public int GetLevelIndex()
    {
        return m_LevelIndex;
    }

    public bool IsEntered()
    {
        return m_Enter;
    }

    private int m_LevelIndex;
    private bool m_Enter;
}

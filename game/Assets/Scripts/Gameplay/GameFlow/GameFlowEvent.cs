public enum EGameFlowAction
{
    Resume,
    Retry,
    Start,
    NextLevel,
    LevelSelection,
    Menu,
    EndLevel,
    EndLevelPanel,
    Quit,
}

public class GameFlowEvent : GameEvent
{
    public GameFlowEvent (EGameFlowAction action) : base ("Game")
    {
        m_Action = action;
    }

    public EGameFlowAction GetAction ()
    {
        return m_Action;
    }

    private EGameFlowAction m_Action;
}
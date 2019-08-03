
public class GameFlowHSM : HSM
{
    public GameFlowHSM()
        : base(new GameFlowMenuState()
              , new GameFlowLevelSelectionState()
              , new GameFlowLevelState()
              , new GameFlowPauseState()
              , new GameFlowEndLevelState()
        )
    {

    }

    public void StartFlow()
    {
        Start(typeof(GameFlowMenuState));
        this.RegisterToUpdate(false, EUpdatePass.Last);
    }

    public void StopFlow()
    {
        this.UnregisterToUpdate(EUpdatePass.Last);
        Stop();
    }
}
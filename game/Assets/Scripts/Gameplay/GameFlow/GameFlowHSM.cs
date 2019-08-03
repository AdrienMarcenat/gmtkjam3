public class GameFlowHSM : HSM
{
    public GameFlowHSM ()
        : base (new GameFlowMenuState ()
              , new GameFlowNormalState ()
              , new GameFlowPauseState ()
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
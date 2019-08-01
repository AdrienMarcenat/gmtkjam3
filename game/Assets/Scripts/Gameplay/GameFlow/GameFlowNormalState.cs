
public class GameFlowNormalState : HSMState
{
    public override void OnEnter ()
    {
        this.RegisterAsListener ("Game", typeof (GameFlowEvent));
    }

    public void OnGameEvent (PlayerInputGameEvent inputEvent)
    {
        if (inputEvent.GetInput () == "Pause" && inputEvent.GetInputState() == EInputState.Down && !UpdaterProxy.Get().IsPaused())
        {
            ChangeNextTransition (HSMTransition.EType.Child, typeof (GameFlowPauseState));
        }
    }
    public void OnGameEvent (GameFlowEvent flowEvent)
    {
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Player");
        this.UnregisterAsListener ("Game");
    }
}
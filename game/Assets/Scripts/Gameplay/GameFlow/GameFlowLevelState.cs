
public class GameFlowLevelState : HSMState
{
    public override void OnEnter ()
    {
        LevelManagerProxy.Get ().LoadLevel();
        this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
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
        switch (flowEvent.GetAction ())
        {
            case EGameFlowAction.EndLevel:
                ChangeNextTransition (HSMTransition.EType.Clear, typeof (GameFlowEndLevelState));
                break;
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Game");
        this.UnregisterAsListener ("Player");
    }
}
﻿public class GameFlowMenuState : HSMState
{
    public override void OnEnter ()
    {
        this.RegisterAsListener ("Game", typeof (GameFlowEvent));
    }

    public void OnGameEvent (GameFlowEvent flowEvent)
    {
        if (flowEvent.GetAction () == EGameFlowAction.Start)
        {
            ChangeNextTransition (HSMTransition.EType.Clear, typeof (GameFlowNormalState));
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Game");
    }
}
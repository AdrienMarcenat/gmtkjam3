using UnityEngine;

public class LevelNameButton : MonoBehaviour
{
    private int m_LevelIndex = 0;

    public void SetLevelIndex(int levelIndex)
    {
        m_LevelIndex = levelIndex;
    }

    public void StartLevel()
    {
        LevelManagerProxy.Get().SetLevelIndex(m_LevelIndex);
        new GameFlowEvent(EGameFlowAction.Start).Push();
    }
}
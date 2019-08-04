using UnityEngine;

public class HintPanel : MonoBehaviour
{
    private int m_GoldCubeCount = 0;

    private void Awake()
    {
        gameObject.SetActive(false);
        this.RegisterAsListener("Game", typeof(GoldCubeCreation), typeof(GoldCubeDestruction), typeof(GameFlowEvent));
    }

    public void OnGameEvent(GoldCubeCreation goldCubeCreationEvent)
    {
        m_GoldCubeCount++;
        gameObject.SetActive(m_GoldCubeCount == 0);

    }

    public void OnGameEvent(GoldCubeDestruction goldCubeDestructionEvent)
    {
        m_GoldCubeCount--;
        gameObject.SetActive(m_GoldCubeCount == 0);
    }

    public void OnGameEvent(GameFlowEvent gameFlowEvent)
    {
        if (gameFlowEvent.GetAction() == EGameFlowAction.EndLevel)
        {
            m_GoldCubeCount = 0;
        }
    }

    private void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }
}


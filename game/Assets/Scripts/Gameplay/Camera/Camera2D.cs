using UnityEngine;

public class Camera2D : MonoBehaviour
{
    private int m_ScreenHeight = 144;
    private int m_PixelPerUnit = 48;

    public void Awake()
    {
        this.RegisterAsListener("Game", typeof(LevelEvent), typeof(GameFlowEvent));
        Reset();
    }

    public void OnDestroy()
    {
        this.UnregisterAsListener("Game");
    }

    public void OnGameEvent(LevelEvent levelEvent)
    {
        if (levelEvent.IsEntered())
        {
            ResizeAndCenter();
        }
    }

    public void OnGameEvent(GameFlowEvent flowEvent)
    {
        switch (flowEvent.GetAction())
        {
            case EGameFlowAction.Menu:
            case EGameFlowAction.LevelSelection:
                Reset();
                break;
        }
    }

    void ResizeAndCenter()
    {
        int levelDimension = LevelManagerProxy.Get().GetLevelDimension();
        float evenCorrection = levelDimension % 2 == 0 ? -0.5f : 0f;
        transform.position = new Vector3(evenCorrection + levelDimension.ToWorldUnit() / 2, levelDimension.ToWorldUnit() / 2, transform.position.z);
        Screen.SetResolution(m_PixelPerUnit * levelDimension, m_ScreenHeight, false);
        float desiredHalfHeight = 0.5f * (1f / m_PixelPerUnit) * m_ScreenHeight;
        GetComponent<Camera>().orthographicSize = desiredHalfHeight;
    }

    private void Reset()
    {
        transform.position = new Vector3(0, 0, -10);
        Screen.SetResolution(800, 600, false);
    }
}

using UnityEngine;

public class Camera2D : MonoBehaviour
{
    public void Awake()
    {
        this.RegisterAsListener("Game", typeof(LevelEvent));
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

    void ResizeAndCenter()
    {
        int levelDimension = LevelManagerProxy.Get().GetLevelDimension();
        float evenCorrection = levelDimension % 2 == 0 ? -0.5f : 0f;
        transform.position = new Vector3(evenCorrection + levelDimension.ToWorldUnit() / 2, levelDimension.ToWorldUnit() / 2, transform.position.z);
        Screen.SetResolution(48 * levelDimension, 3*48, false);
    }
}

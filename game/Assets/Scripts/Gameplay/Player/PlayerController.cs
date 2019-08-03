using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class PlayerController : MonoBehaviour
{
    private MovingObject m_Mover;
    private bool m_ProcessInput = true;
    private Vector3 m_FacingDirection;

    private static Vector3 ms_Right = new Vector3 (1, 0, 0);
    private static Vector3 ms_Left = new Vector3 (-1, 0, 0);

    void Awake ()
    {
        m_Mover = GetComponent<MovingObject> ();
        m_FacingDirection = new Vector3 (1, 0, 0);
        this.RegisterAsListener ("Player", typeof(PlayerInputGameEvent));
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent(PlayerInputGameEvent inputEvent)
    {
        if (UpdaterProxy.Get().IsPaused() || !m_ProcessInput)
        {
            return;
        }
        string input = inputEvent.GetInput ();
        EInputState state = inputEvent.GetInputState ();
        switch (input)
        {
            case "Undo":
                if (state == EInputState.Down)
                {
                    Undo ();
                }
                break;
            case "Right":
                Move (state == EInputState.Held ? 1 : 0);
                m_FacingDirection = ms_Right;
                break;
            case "Left":
                Move (state == EInputState.Held ? -1 : 0);
                m_FacingDirection = ms_Left;
                break;
            default:
                break;
        }
    }

    private void Undo()
    {
    }

    private void Move (float xDir)
    {
    }
}

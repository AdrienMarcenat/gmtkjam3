using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MovingObject))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int m_UndoPerSecond = 10;
    [SerializeField] private float m_HeldUndoTreshold = 2f;

    private EDirection m_FacingDirection = EDirection.Right;
    private bool m_IsUndoing = false;
    private float m_TimeHoldingUndo = 0f;
    private Player m_PlayerTileObject;

    void Awake ()
    {
        m_PlayerTileObject = GetComponent<Player>();
        this.RegisterAsListener ("Player", typeof(PlayerInputGameEvent));
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent(PlayerInputGameEvent inputEvent)
    {
        if (UpdaterProxy.Get().IsPaused())
        {
            return;
        }
        string input = inputEvent.GetInput();
        EInputState state = inputEvent.GetInputState();
        if (state == EInputState.Held || state == EInputState.Down)
        {
            switch (input)
            {
                case "Right":
                    m_PlayerTileObject.AddMoveCommand(1, 0);
                    break;
                case "Left":
                    m_PlayerTileObject.AddMoveCommand(-1, 0);
                    break;
                case "Up":
                    m_PlayerTileObject.AddMoveCommand(0, 1);
                    break;
                case "Down":
                    m_PlayerTileObject.AddMoveCommand(0, -1);
                    break;
                case "Undo":
                    m_TimeHoldingUndo += Time.deltaTime;
                    if (!m_IsUndoing && (m_TimeHoldingUndo > m_HeldUndoTreshold || state == EInputState.Down))
                    {
                        StartCoroutine(Undo());
                    }
                    break;
                default:
                    break;
            }
            if (state == EInputState.Up && input == "Undo")
            {
                m_TimeHoldingUndo = 0f;
            }
        }
    }

    IEnumerator Undo ()
    {
        m_IsUndoing = true;
        yield return new WaitForSeconds (1f / m_UndoPerSecond);
        CommandStackProxy.Get ().Undo ();
        m_IsUndoing = false;
    }
}

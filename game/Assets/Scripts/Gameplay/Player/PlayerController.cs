using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int m_UndoPerSecond = 10;
    [SerializeField] private int m_RapidUndoPerSecond = 20;
    [SerializeField] private float m_HeldUndoTreshold = 2f;

    private EDirection m_FacingDirection = EDirection.Right;
    private bool m_IsUndoing = false;
    private float m_TimeHoldingUndo = 0f;
    private MovingTileObject m_MovingTileObject;

    void Awake ()
    {
        m_MovingTileObject = GetComponent<MovingTileObject>();
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
                    TryMove(1, 0);
                    break;
                case "Left":
                    TryMove(-1, 0);
                    break;
                case "Up":
                    TryMove(0, 1);
                    break;
                case "Down":
                    TryMove(0, -1);
                    break;
                case "Undo":
                    m_TimeHoldingUndo += Time.deltaTime;
                    if (!m_IsUndoing)
                    {
                        StartCoroutine(Undo(m_TimeHoldingUndo > m_HeldUndoTreshold));
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

    IEnumerator Undo (bool rapidUndo)
    {
        m_IsUndoing = true;
        int undoRate = rapidUndo ? m_RapidUndoPerSecond : m_UndoPerSecond;
        yield return new WaitForSeconds (1f / undoRate);
        // Becauses we evalaute rules after moving tileobject we must do the reverse here
        new UndoTileEvent().Push();
        TileManagerProxy.Get().UpdateTileObjects();
        new UndoTileObjectEvent().Push();
        TileManagerProxy.Get().UpdateTileObjects();
        m_IsUndoing = false;
    }

    public void TryMove(int xDir, int yDir)
    {
        if (m_MovingTileObject.TryMove(xDir, yDir))
        {
            new MoveEvent().Push();
            TileManagerProxy.Get().UpdateTileObjects();
            new EvaluateRuleEvent().Push();
            TileManagerProxy.Get().UpdateTileObjects();
        }
    }
}

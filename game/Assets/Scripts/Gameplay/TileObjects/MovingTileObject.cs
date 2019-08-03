using System.Collections;
using UnityEngine;

public class MovingTileObject : TileObject
{
    [SerializeField] private float m_MoveSpeed = 300f;

    private bool m_IsMoving = false;
    private Vector3 m_TargetPos;

    public void AddMoveCommand(int xDir, int yDir)
    {
        if (CanMoveTo(xDir, yDir))
        {
            MoveCommand command = new MoveCommand(gameObject, xDir, yDir);
            command.Execute();
            CommandStackProxy.Get().PushCommand(command);
        }
    }

    public void Move(int xDir, int yDir)
    {
        SetTargetPos(xDir, yDir);
        StartCoroutine(MoveRoutine());
    }

    public void MoveInstant(int xDir, int yDir)
    {
        StopMovement();
        SetTargetPos(xDir, yDir);
        transform.position = m_TargetPos;
    }

    private void SetTargetPos(int xDir, int yDir)
    {
        m_TargetPos = new Vector3(transform.position.x + xDir.ToWorldUnit(), transform.position.y + yDir.ToWorldUnit(), transform.position.z);
    }

    private void SetIsMoving(bool isMoving)
    {
        m_IsMoving = isMoving;
    }

    IEnumerator MoveRoutine()
    {
        SetIsMoving(true);
        while (transform.position != m_TargetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, Time.deltaTime * m_MoveSpeed);
            yield return null;
        }
        SetIsMoving(false);
    }

    private void StopMovement()
    {
        if (m_IsMoving)
        {
            StopAllCoroutines();
            SetIsMoving(false);
            transform.position = m_TargetPos;
        }
    }

    private bool CanMoveTo(int xDir, int yDir)
    {
        if (m_IsMoving)
        {
            return false;
        }
        return true;
    }
}

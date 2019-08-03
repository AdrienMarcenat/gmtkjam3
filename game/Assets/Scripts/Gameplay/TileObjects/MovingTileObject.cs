using System.Collections;
using UnityEngine;

public class MovingTileObject : TileObject
{
    [SerializeField] private float m_MoveSpeed = 10f;

    private bool m_IsMoving = false;
    private Vector3 m_TargetPos;

    public bool TryMove(int xDir, int yDir)
    {
        if (EvaluateMoveTo(xDir, yDir))
        {
            AddMoveCommand(xDir, yDir);
            return true;
        }
        return false;
    }

    public override void AddMoveCommand(int xDir, int yDir)
    {
        MoveCommand command = new MoveCommand(gameObject, xDir, yDir);
        TileCoordinates targetCoordinate = GetCoordinates() + new TileCoordinates(xDir, yDir);
        TileObject objectInTargetTile = TileManagerProxy.Get().GetObjectInTile(targetCoordinate);
        if (objectInTargetTile != null)
        {
            // Propagate the move (push objects)
            objectInTargetTile.AddMoveCommand(xDir, yDir);
        }
        command.Execute();
        PushCommand(command);
        m_HasAlreadyAddedCommand = true;
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
        SnapOnCurrentCoordinates();
    }

    private void SetTargetPos(int xDir, int yDir)
    {
        TileCoordinates targetCoordinate = GetCoordinates() + new TileCoordinates(xDir, yDir);
        SetCoordinates(targetCoordinate);
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
        SnapOnCurrentCoordinates();
        SetIsMoving(false);
    }

    private void SnapOnCurrentCoordinates()
    {
        TileCoordinates currentCoordinates = GetCoordinates();
        Vector3 currentPosition = new Vector3(currentCoordinates.x, currentCoordinates.y, 0);
        transform.position = currentPosition;
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
    public override void OnTeleport()
    {
        StopMovement();
    }

    public override bool EvaluateMoveTo(int xDir, int yDir)
    {
        if (m_IsMoving)
        {
            return false;
        }
        TileCoordinates targetCoordinate = GetCoordinates() + new TileCoordinates(xDir, yDir);
        Tile targetTile = TileManagerProxy.Get().GetTile(targetCoordinate);
        if(targetTile != null && targetTile.IsObstacle())
        {
            return false;
        }
        TileObject objectInTargetTile = TileManagerProxy.Get().GetObjectInTile(targetCoordinate);
        if(objectInTargetTile != null && !objectInTargetTile.EvaluateMoveTo(xDir, yDir))
        {
            return false;
        }
        return true;
    }
}

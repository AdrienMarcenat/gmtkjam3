using UnityEngine;

public class TeleporterBase : Tile
{
    [SerializeField] protected TeleporterBase m_OtherEnd;

    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        if (tileObject != null && tileObject.GetObjectType() == ETileObjectType.Cube)
        {
            return m_OtherEnd.CanReceive();
        }
        return false;
    }

    private bool CanReceive()
    {
        return GetTileObject() == null;
    }

    public void SetOtherEnd(TeleporterBase teleporter)
    {
        m_OtherEnd = teleporter;
    }

    public void Teleport()
    {
        TileObject tileObject = GetTileObject();
        if (tileObject != null && tileObject.GetObjectType() == ETileObjectType.Cube)
        {
            if (m_OtherEnd.CanReceive())
            {
                TileCoordinates receiverCoordinates = m_OtherEnd.GetCoordinates();
                tileObject.OnTeleport();
                tileObject.transform.position = m_OtherEnd.transform.position;
                tileObject.SetCoordinates(receiverCoordinates);
            }
        }
    }
}

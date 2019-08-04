using UnityEngine;

public class TeleporterBase : Tile
{
    private TeleporterBase m_OtherEnd;
    private TileCoordinates m_OtherEndCoordinate;

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        if (args.Length > 0)
        {
            int otherEndX = int.Parse(args[0]);
            m_OtherEndCoordinate = new TileCoordinates(otherEndX, y);
            GetComponent<SpriteRenderer>().sprite = RessourceManager.LoadSprite(args[1], 0);
        }
    }

    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        if (tileObject != null && tileObject.IsCube())
        {
            return GetOtherEnd().CanReceive();
        }
        return false;
    }

    private bool CanReceive()
    {
        return GetTileObject() == null;
    }

    protected TeleporterBase GetOtherEnd()
    {
        if (m_OtherEnd == null)
        {
            Tile otherEnd = TileManagerProxy.Get().GetTile(m_OtherEndCoordinate);
            m_OtherEnd = (TeleporterBase)otherEnd;
        }
        return m_OtherEnd;
    }

    public void Teleport()
    {
        TileObject tileObject = GetTileObject();
        TeleporterBase otherEnd = GetOtherEnd();
        if (tileObject != null && tileObject.IsCube())
        {
            if (otherEnd.CanReceive())
            {
                TileCoordinates receiverCoordinates = otherEnd.GetCoordinates();
                tileObject.OnTeleport();
                tileObject.transform.position = otherEnd.transform.position;
                tileObject.SetCoordinates(receiverCoordinates);
            }
        }
    }
}

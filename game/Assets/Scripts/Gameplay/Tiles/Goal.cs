
public class Goal : Tile
{
    public override void EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        if (tileObject != null && tileObject.GetObjectType() == ETileObjectType.Cube)
        {
            new GameFlowEvent(EGameFlowAction.Win).Push();
            this.DebugLog("Win");
        }
    }
}

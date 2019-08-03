
public class Goal : Tile
{
    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        return tileObject != null && tileObject.GetObjectType() == ETileObjectType.Cube;
    }

    public override void DoRule()
    {
        new GameFlowEvent(EGameFlowAction.Win).Push();
        this.DebugLog("Win");
    }
}

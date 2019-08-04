
public class Goal : Tile
{
    public override bool EvaluateRule()
    {
        TileObject tileObject = GetTileObject();
        return tileObject != null 
            && tileObject.GetObjectType() == ETileObjectType.Cube 
            && tileObject.tag == "GoldCube";
    }

    public override void DoRule()
    {
        new GameFlowEvent(EGameFlowAction.EndLevel).Push();
        this.DebugLog("Win");
    }
}


public class Emitter : TeleporterBase
{
    public override void DoRule()
    {
        Teleport();
    }

    public override void UndoRule()
    {
        m_OtherEnd.Teleport();
    }
}

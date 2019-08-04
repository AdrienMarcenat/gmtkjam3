using UnityEngine;

public class Emitter : TeleporterBase
{
    [SerializeField] private AudioClip m_TeleportSound;
    private GameObject m_EmitterAnimationPrefab;
    private GameObject m_ReceiverAnimationPrefab;

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        m_EmitterAnimationPrefab = RessourceManager.LoadPrefab("EmitterAnimation");
        m_ReceiverAnimationPrefab = RessourceManager.LoadPrefab("ReceiverAnimation");
    }

    public override void DoRule()
    {
        Teleport();
        GameObject emitterAnimationPrefab = Instantiate(m_EmitterAnimationPrefab);
        emitterAnimationPrefab.transform.position = transform.position;
        GameObject receiverAnimationPrefab = Instantiate(m_ReceiverAnimationPrefab);
        receiverAnimationPrefab.transform.position = GetOtherEnd().transform.position;
        SoundManagerProxy.Get().PlayMultiple(m_TeleportSound);
    }

    public override void UndoRule()
    {
        GetOtherEnd().Teleport();
    }
}

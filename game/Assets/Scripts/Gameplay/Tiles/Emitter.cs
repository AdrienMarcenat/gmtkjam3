using UnityEngine;

public class Emitter : TeleporterBase
{
    [SerializeField] private AudioClip m_TeleportSound;
    [SerializeField] private GameObject m_EmitterAnimationPrefab;
    [SerializeField] private GameObject m_ReceiverAnimationPrefab;

    public override void Init(ETileType type, int x, int y, string[] args)
    {
        base.Init(type, x, y, args);
        m_EmitterAnimationPrefab = RessourceManager.LoadPrefab("EmitterAnimation");
        m_ReceiverAnimationPrefab = RessourceManager.LoadPrefab("ReceiverAnimation");
    }

    public override void DoRule()
    {
        Teleport();
        if (m_EmitterAnimationPrefab != null)
        {
            GameObject emitterAnimationPrefab = Instantiate(m_EmitterAnimationPrefab);
            emitterAnimationPrefab.transform.position = new Vector3(
                transform.position.x,
                transform.position.y+0.5f,
                transform.position.z);
        }
        if (m_ReceiverAnimationPrefab != null)
        {
            GameObject receiverAnimationPrefab = Instantiate(m_ReceiverAnimationPrefab);
            Vector3 otherPosition = GetOtherEnd().transform.position;
            receiverAnimationPrefab.transform.position = new Vector3(
                otherPosition.x,
                otherPosition.y + 0.5f,
                otherPosition.z);
        }
        SoundManagerProxy.Get().PlayMultiple(m_TeleportSound);
    }

    public override void UndoRule()
    {
        GetOtherEnd().Teleport();
    }
}

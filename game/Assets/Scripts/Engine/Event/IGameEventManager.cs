
using AnyObject = System.Object;
public interface IGameEventManager
{
    void Register(AnyObject objectToNotify, string tag, params System.Type[] GameEventTypes);

    void Unregister(AnyObject objectToNotify, string tag);

    void ToggleListener(AnyObject objectToToggle, string tag, bool toggle);

    void PushGameEvent(GameEvent e, GameEvent.EProtocol protocol);
}

public class GameEventManagerProxy : UniqueProxy<IGameEventManager>
{ }
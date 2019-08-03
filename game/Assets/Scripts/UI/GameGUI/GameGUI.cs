using UnityEngine;
using UnityEngine.UI;

public class GameGUI : MonoBehaviour
{
    [SerializeField] private Text m_LevelName;

    private void Start ()
    {
        m_LevelName.text = LevelManagerProxy.Get ().GetCurrentLevelName ();
    }
}

using UnityEngine;

public class LevelPositions : MonoBehaviour
{
    [SerializeField] private Vector3 m_Spacing;
    [SerializeField] private GameObject m_LevelPositonPrefab;

    private void Awake ()
    {
        Vector3 levelpos = Vector3.zero;
        for (int i = 0; i < LevelManagerProxy.Get ().GetLevelNames ().Count; i++)
        {
            GameObject level = GameObject.Instantiate (m_LevelPositonPrefab);
            level.transform.SetParent (transform, false);
            level.transform.position = levelpos;
            levelpos += m_Spacing;
        }
    }
}

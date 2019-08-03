using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelPrefab;
    [SerializeField] private Text m_BestNumber;
    [SerializeField] private Text m_CurrenLevelName;
    [SerializeField] private Vector3 m_Offset;

    private Transform[] m_LevelPositions;

    private void Start ()
    {
        m_LevelPositions = GameObject.Find ("LevelPositions").GetComponentsInChildren<Transform> ().SubArray (1);
        Dictionary<int, string> levelIdToNames = LevelManagerProxy.Get ().GetLevelNames ();
        foreach (int id in levelIdToNames.Keys)
        {
            GameObject level = GameObject.Instantiate (m_LevelPrefab);
            level.transform.position = m_LevelPositions[id].position + m_Offset;
        }
    }

    private void Update ()
    {
        m_CurrenLevelName.text = LevelManagerProxy.Get ().GetCurrentLevelName ();
        if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Space))
        {
            new GameFlowEvent (EGameFlowAction.Start).Push ();
        }
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            new GameFlowEvent (EGameFlowAction.Menu).Push ();
        }
    }
}

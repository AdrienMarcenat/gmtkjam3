using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelectionMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_LevelPrefab;

    private void Start ()
    {
        bool isFirstLevel = true;
        Dictionary<int, string> levelIdToNames = LevelManagerProxy.Get ().GetLevelNames ();
        foreach (int id in levelIdToNames.Keys)
        {
            GameObject level = GameObject.Instantiate (m_LevelPrefab, transform);
            Text levelNameText = level.GetComponentInChildren<Text>();
            levelNameText.text = levelIdToNames[id];
            LevelNameButton button = level.GetComponent<LevelNameButton>();
            button.SetLevelIndex(id);
            if (isFirstLevel)
            {
                level.AddComponent<ButtonFirstSelected>();
                isFirstLevel = false;
            }
        }
    }

    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
        {
            new GameFlowEvent (EGameFlowAction.Menu).Push ();
        }
    }
}

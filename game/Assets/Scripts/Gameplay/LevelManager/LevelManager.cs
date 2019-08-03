
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager
{
    private int m_CurrentLevel = 0;
    private int m_CurrentLevelDimension;
    private Dictionary<int, string> m_LevelIdToName;

    private static string ms_LevelFilename = "/LevelNames.txt";

    public LevelManager ()
    {
        m_CurrentLevelDimension = 0;
        m_LevelIdToName = new Dictionary<int, string> ();
        FillLevelNames (ms_LevelFilename);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    ~LevelManager ()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadScene (int sceneIndex)
    {
        new LevelEvent (m_CurrentLevel, false).Push ();
        SceneManager.LoadScene (sceneIndex);
    }

    public void LoadScene (string sceneName)
    {
        new LevelEvent (m_CurrentLevel, false).Push ();
        SceneManager.LoadScene (sceneName);
    }

    public void LoadLevel ()
    {
        LoadScene ("Scenes/Level");
    }

    public void SetLevelIndex (int levelIndex)
    {
        m_CurrentLevel = levelIndex;
    }

    public bool IsLastLevel ()
    {
        return m_CurrentLevel == m_LevelIdToName.Count - 1;
    }

    public void NextLevel ()
    {
        if (!IsLastLevel ())
        {
            m_CurrentLevel++;
        }
    }

    public string GetActiveSceneName ()
    {
        return SceneManager.GetActiveScene ().name;
    }

    public string GetCurrentLevelName ()
    {
        return m_LevelIdToName[m_CurrentLevel];
    }

    public int GetCurrentLevelID ()
    {
        return m_CurrentLevel;
    }

    public Dictionary<int, string> GetLevelNames ()
    {
        return m_LevelIdToName;
    }

    public void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        if (GetActiveSceneName () == "Level")
        {
            TileManagerProxy.Get ().Reset ();
            string levelName = GetCurrentLevelName ();
            m_CurrentLevelDimension = LevelParser.GenLevel ("/" + levelName + ".txt");
            TileCoordinates.ms_Modulo = m_CurrentLevelDimension;
            new LevelEvent (m_CurrentLevel, true).Push ();
        }
    }

    public int GetLevelDimension ()
    {
        return m_CurrentLevelDimension;
    }

    private void FillLevelNames (string filename)
    {
        char[] separators = { ':' };
        filename = Application.streamingAssetsPath + filename;

        string[] lines = File.ReadAllLines (filename);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split (separators);

            // If there is an error in print a debug message
            if (datas.Length != 2)
            {
                this.DebugLog ("Invalid number of data line " + i + " expecting 2, got " + datas.Length);
                return;
            }

            int levelIndex = Int32.Parse ((String)datas.GetValue (0)); ;
            string levelName = datas[1];
            m_LevelIdToName.Add (levelIndex, levelName);
        }
    }

    public void OnLevelEnd ()
    {
    }
}

public class LevelManagerProxy : UniqueProxy<LevelManager>
{ }

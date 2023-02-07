using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameManager : MonoBehaviour
{
    private string playerName;
    private int topScore;
    [SerializeField] private GameObject inputField;
    private static NameManager Instance;
    string path; 
    
    private void Awake()
    {
        path = Application.persistentDataPath + "/record.json";
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        if (File.Exists(path))
        {
            LoadNameScore();
            Debug.Log(playerName + " " + topScore);
        }
        
    }

    public void FormName()
    {
        playerName = inputField.GetComponent<InputField>().text;
    }

    public static NameManager GetInstance()
    {
        return Instance;
    }

    public string GetName()
    {
        return playerName;
    }

    public void LoadNameScore()
    {
        string json = File.ReadAllText(path);
        MainManager.NameScore data = JsonUtility.FromJson<MainManager.NameScore>(json);
        playerName = data.playerName;
        topScore = data.score;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
        
            Application.Quit();
        #endif
        
    }
}

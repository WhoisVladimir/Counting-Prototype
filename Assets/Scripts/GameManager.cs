using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singletone<GameManager>
{
    //public static GameManager Instance { get; private set; }
    public bool IsGameActive { get; private set; }

    string curLevelName;
   // todo:
   // Загрузка необходимой сцены
   // Управлние менеджерами
   // Завершение игры
    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        IsGameActive = true;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadLevel("Main Menu");
    }

    void OnEnable()
    {
        Player.OnDeath += GameOver;
    }
    void OnDisable()
    {
        Player.OnDeath -= GameOver;
    }

    void GameOver()
    {
        IsGameActive = false;
    }

    void LoadLevel(string lvlName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(lvlName, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        curLevelName = lvlName;
    }

    void UnLoadLevel(string lvlName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(lvlName);
        ao.completed += OnUnloadOperationComplete;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Load complete");
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

}

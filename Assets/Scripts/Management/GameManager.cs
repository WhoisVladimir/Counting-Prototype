using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singletone<GameManager>
{
    public static event Action OnSceneReadiness;
    public static GameState CurrentGameState;
    [SerializeField] GameObject[] SystemPrefabs;
    List<GameObject> instancedSystemPrefabs;
    public bool IsGameActive { get; private set; }
    string curLevelName;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        IsGameActive = true;
        DontDestroyOnLoad(gameObject);
        CurrentGameState = GameState.PreGame;
        instancedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
    }

    void Start()
    {
        LoadLevel("Main Menu");
    }

    void OnEnable()
    {
        Player.OnDeath += GameOver;
        MainMenu.OnStartButtonClick += MainMenu_OnStartButtonClick;
    }

    void OnDisable()
    {
        Player.OnDeath -= GameOver;
        MainMenu.OnStartButtonClick -= MainMenu_OnStartButtonClick;
    }

    void GameOver()
    {
        IsGameActive = false;
    }

    void LoadLevel(string lvlName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(lvlName, LoadSceneMode.Additive);
        ao.completed += OnLoadOperationComplete;
        if (curLevelName != null) UnLoadLevel(curLevelName);
        curLevelName = lvlName;
    }

    void UnLoadLevel(string lvlName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(lvlName);
        ao.completed += OnUnloadOperationComplete;
    }

    public void RestartGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Boot"));
        UnLoadLevel(curLevelName);
        curLevelName = null;
        LoadLevel("Game Scene");
        IsGameActive = true;
    }
    void OnLoadOperationComplete(AsyncOperation ao)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(curLevelName));
        OnSceneReadiness?.Invoke();
    }
    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    void InstantiateSystemPrefabs()
    {
        GameObject systemPrefab;
        for (int i = 0; i < SystemPrefabs.Length; i++)
        {
            systemPrefab = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(systemPrefab);
        }
    }

    private void MainMenu_OnStartButtonClick()
    {
        LoadLevel("Game Scene");
        CurrentGameState = GameState.Game;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }
        instancedSystemPrefabs.Clear();   
    }
}

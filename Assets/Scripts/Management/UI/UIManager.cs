using UnityEngine;
using System.Collections.Generic;

public class UIManager : Singletone<UIManager>
{
    [SerializeField] GameObject[] PreGameUIPrefabs;
    [SerializeField] GameObject[] GameUIPrefabs;
    List<GameObject> instancedUIPrefabs;
    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        instancedUIPrefabs = new List<GameObject>();
    }
    private void OnEnable()
    {
        GameManager.OnSceneReadiness += GameManager_OnSceneReadiness;
    }

    private void OnDisable()
    {
        GameManager.OnSceneReadiness -= GameManager_OnSceneReadiness;
    }

    void InstantiateUIPrefabs()
    {
        GameObject UIPrefab;
        ClearUI();
        switch (GameManager.CurrentGameState)
        {
            case GameState.PreGame:
                foreach (var item in PreGameUIPrefabs)
                {
                    UIPrefab = Instantiate(item);
                    instancedUIPrefabs.Add(UIPrefab);
                }
                break;

            case GameState.Game:
                foreach (var item in GameUIPrefabs)
                {
                    UIPrefab = Instantiate(item);
                    instancedUIPrefabs.Add(UIPrefab);
                }
                break;
        }
    }

    private void GameManager_OnSceneReadiness()
    {
        InstantiateUIPrefabs();
    }

    void ClearUI()
    {
        if (instancedUIPrefabs.Count > 0)
        {
            foreach (var item in instancedUIPrefabs)
            {
                Destroy(item);
            }
            instancedUIPrefabs.Clear();
        }
    }
}

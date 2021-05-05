using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singletone<PoolManager>
{
    [SerializeField] List<GameObject> prefabs;
    public List<GameObject> Prefabs => prefabs;
    int prefabCount = 1;
    public Dictionary<GameObject, List<GameObject>> prefabDictionary;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        prefabDictionary = new Dictionary<GameObject, List<GameObject>>();
    }
    void OnEnable()
    {
        GameManager.OnSceneReadiness += GameManager_OnSceneReadiness;
    }
    void OnDisable()
    {
        GameManager.OnSceneReadiness -= GameManager_OnSceneReadiness;
    }
    Dictionary<GameObject, List<GameObject>> FillDictionary()
    {
        foreach (var prefab in prefabs)
        {
            List<GameObject> pool = GeneratePool(prefabCount, prefab);
            prefabDictionary.Add(prefab, pool);
        }
        return prefabDictionary;
    }
    public List<GameObject> GeneratePool(int amount, GameObject prefab)
    {
        List<GameObject> itemPool = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            itemPool.Add(obj);
        }
        return itemPool;
    }

    public GameObject GetPoolObject(GameObject keyObj)
    {
        List<GameObject> pool = prefabDictionary[keyObj];

        foreach (var item in pool)
        {
            if (!item.activeInHierarchy) return item;
        }
        GameObject obj = prefabs.Find(item => item.Equals(keyObj));
        obj = Instantiate(obj);
        pool.Add(obj);
        return obj;
    }

    private void GameManager_OnSceneReadiness()
    {
       if(GameManager.CurrentGameState == GameState.Game) prefabDictionary = FillDictionary();
    }

}

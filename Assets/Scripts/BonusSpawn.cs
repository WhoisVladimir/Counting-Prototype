using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawn : MonoBehaviour
{
    static BonusSpawn instance;
    public static BonusSpawn Instance => instance;
    GameObject bonus;
    Vector3 spawnPosition;

    private void OnEnable()
    {
        BubbleLogic.OnHit += SpawnBonus; 
    }
    private void OnDisable()
    {
        BubbleLogic.OnHit -= SpawnBonus;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        bonus = PoolManager.Instance.Prefabs.Find(obj => obj.CompareTag("Bonus"));
    }
    public void FixSpawnInformation(Vector3 ballPosition)
    {
        spawnPosition = ballPosition;
    }
    void SpawnBonus()
    {
        GameObject obj = PoolManager.Instance.GetPoolObject(bonus.name);
        obj.transform.position = spawnPosition;
        obj.SetActive(true);
    }
}

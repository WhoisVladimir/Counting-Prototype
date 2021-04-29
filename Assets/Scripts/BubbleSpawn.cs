using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : Singletone<BubbleSpawn>
{
    List<GameObject> balls;

    GameObject currentBall;
    Vector3 spawnPos;

    int countBalls;
    int stage = 1;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();

        balls = new List<GameObject>();
    }

    private void Start()
    {
        FillBallsList();
        spawnPos = new Vector3(Random.Range(-17f, 17f), 7f, 0f);
        SpawnBall(0);
    }
    public void FixSpawnInformation(Vector3 lastPosition, int type)
    {
        countBalls--;
        spawnPos = lastPosition;
        if (type < 4)
        {
            type++;
            for (int i = 0; i < 2; i++) SpawnBall(type);
        }
        if (countBalls == 0)
        {
            stage++;
            while (countBalls < stage) SpawnBall(0);
        }
    }

    void SpawnBall(int type)
    {
        currentBall = balls[type];
        currentBall = PoolManager.Instance.GetPoolObject(currentBall.name);
        currentBall.transform.position = spawnPos;
        currentBall.SetActive(true);
        countBalls++;
    }

    void FillBallsList()
    {
        balls = PoolManager.Instance.Prefabs.FindAll(prefab => prefab.CompareTag("Bubble"));
    }
}

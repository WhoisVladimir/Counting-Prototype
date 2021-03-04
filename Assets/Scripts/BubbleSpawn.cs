using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    List<GameObject> balls;

    GameObject currentBall;
    Vector3 spawnPos;
    static BubbleSpawn instance;
    public static BubbleSpawn Instance => instance;

    int countBalls;
    int stage = 1;

    private void Awake()
    {
        instance = this;
        balls = new List<GameObject>();
    }

    private void Start()
    {
        FillBallsQueue();
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

    void FillBallsQueue()
    {
        balls = PoolManager.Instance.Prefabs.FindAll(prefab => prefab.CompareTag("Bubble"));
    }
}

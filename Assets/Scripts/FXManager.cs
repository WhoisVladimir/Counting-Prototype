using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    static FXManager instance;
    public static FXManager Instance => instance;

    GameObject effect;
    Vector3 spawnPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        effect = PoolManager.Instance.Prefabs.Find(obj => obj.CompareTag("FX"));
    }

    private void OnEnable()
    {
        BubbleLogic.OnHit += Explosion;
        Player.OnDeath += Player_OnDeath;
    }
    private void OnDisable()
    {
        BubbleLogic.OnHit -= Explosion;
        Player.OnDeath -= Player_OnDeath;
    }

    private void Player_OnDeath()
    {
        StopAllCoroutines();
    }

    public void FixSpawnInformation(Vector3 ballPos)
    {
        spawnPosition = ballPos;
    }

    void Explosion()
    {
        GameObject obj = PoolManager.Instance.GetPoolObject(effect.name);
        obj.transform.position = spawnPosition;
        obj.SetActive(true);
        obj.GetComponent<AudioSource>().Play();
        StartCoroutine(FXDuration(obj));
    }

    IEnumerator FXDuration(GameObject obj)
    {
        yield return new WaitWhile(() => obj.GetComponent<ParticleSystem>().isPlaying);
        obj.SetActive(false);
    }
}

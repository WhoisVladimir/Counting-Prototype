using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singletone<PlayerController>
{
    public static event Action OnShot;
    [SerializeField] List<GameObject> bullets;
    GameObject currentItem;
    int prefabNumber = 0;
    string bulletName;
    bool isTimeToShot = true;
    Rigidbody playerRb;
    [SerializeField] float speed;
    float horizontalMove;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
        playerRb = GetComponent<Rigidbody>();
        currentItem = bullets[prefabNumber];
    }
    private void FixedUpdate()
    {
        playerRb.velocity = Vector3.right * horizontalMove * speed;
    }

    void Update()
    {
        MoveRb();
        if (Input.GetKeyDown(KeyCode.Space) && isTimeToShot)
        {
            isTimeToShot = false;
            StartCoroutine("Fire");
        }
    }

    private void MoveRb()
    {
        horizontalMove = Input.GetAxis("Horizontal");
    }

    IEnumerator Fire()
    {
        GameObject shell = PoolManager.Instance.GetPoolObject(currentItem);
        shell.transform.position = transform.position;
        float tempTimer = shell.GetComponent<BulletLogic>().Timer;
        shell.SetActive(true);
        OnShot?.Invoke();
        yield return new WaitForSeconds(tempTimer);
        isTimeToShot = true;
    }
}

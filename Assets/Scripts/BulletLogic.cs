using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Action();

public class BulletLogic : MonoBehaviour
{
    Rigidbody shellRb;
    [SerializeField] float speed;
    [SerializeField] float timer;
    public float Timer { get { return timer; }}

        
    private void Awake()
    {
        shellRb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        shellRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Bonus"))
        {
            shellRb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}

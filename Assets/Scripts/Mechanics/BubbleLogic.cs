using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLogic : MonoBehaviour
{
    Rigidbody ballRb;
    [SerializeField] float speed;
    [SerializeField] int ballType;
   
    public static event Action OnHit;

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        ballRb.AddForce(Vector3.up * speed, ForceMode.Impulse);
        Player.OnDeath += Player_OnDeath;
    }
    private void OnDisable()
    {
        Player.OnDeath -= Player_OnDeath;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 direction = transform.position - collision.transform.position;
        ballRb.AddForceAtPosition(direction.normalized * speed, collision.transform.position, ForceMode.Impulse);
        if (ballRb.velocity.magnitude < 13f) ballRb.velocity *= 2f;
        else if (ballRb.velocity.magnitude > 35f) ballRb.velocity /= 1.5f;
        gameObject.GetComponent<AudioSource>().Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            FXManager.Instance.FixSpawnInformation(transform.position);
            BonusSpawn.Instance.FixSpawnInformation(transform.position);
            OnHit?.Invoke();
            BubbleSpawn.Instance.FixSpawnInformation(transform.position, ballType);
            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Bounds"))
        {
            ballRb.transform.position = new Vector3(0, 11f, 0);
        }
    }
    private void Player_OnDeath()
    {
        gameObject.SetActive(false);
    }

}

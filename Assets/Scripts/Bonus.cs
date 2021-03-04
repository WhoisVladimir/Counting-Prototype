using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public static event Action OnBonusPickup;
    Rigidbody bonusRb;

    private void Awake()
    {
        bonusRb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        bonusRb.useGravity = true;
        Player.OnDeath += Player_OnDeath;
    }
    private void OnDisable()
    {
        Player.OnDeath -= Player_OnDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Environment"))
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            bonusRb.useGravity = false;
            bonusRb.velocity = Vector3.zero;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            OnBonusPickup?.Invoke();
            gameObject.SetActive(false);
        }
    }
    private void Player_OnDeath()
    {
        gameObject.SetActive(false);
    }

}

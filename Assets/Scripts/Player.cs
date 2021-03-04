using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Player instance;
    public static Player Instance => instance;

    public static event Action OnDeath; 
    int health = 100;
    public int Health => health;
    [SerializeField] AudioClip oi_SFX;
    [SerializeField] AudioClip bonus_SFX;
    AudioSource audioSource;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Bonus.OnBonusPickup += AddBonus;
    }
    void OnDisable()
    {
        Bonus.OnBonusPickup -= AddBonus;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bubble"))
        {
            audioSource.PlayOneShot(oi_SFX);
            if (health > 10)
            {
                health -= 10;
            }
            else
            {
                health -= 10;
                OnDeath?.Invoke();
                gameObject.SetActive(false);
            }
        }
    }
    void AddBonus()
    {
        health += 10;
        audioSource.PlayOneShot(bonus_SFX);
    }
}

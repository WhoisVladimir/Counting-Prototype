using UnityEngine;

public class Player : Singletone<Player>
{
    public static event Action OnDeath; 
    static int health;
    public static int Health => health;
    [SerializeField] AudioClip oi_SFX;
    [SerializeField] AudioClip bonus_SFX;
    AudioSource audioSource;
    protected override void Awake()
    {
        health = 100;
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
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

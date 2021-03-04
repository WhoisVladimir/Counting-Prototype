using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsGameActive { get; private set; }
   // todo:
   // Загрузка необходимой сцены
   // Управлние менеджерами
   // Завершение игры
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        IsGameActive = true;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Player.OnDeath += GameOver;
    }
    void OnDisable()
    {
        Player.OnDeath -= GameOver;
    }

    void GameOver()
    {
        IsGameActive = false;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

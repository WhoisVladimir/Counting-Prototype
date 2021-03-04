using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOvText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] Button restartButton;
    int score = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        BubbleLogic.OnHit += ScoreUpdate;
        Player.OnDeath += GameOverUI;

    }
    private void OnDisable()
    {
        BubbleLogic.OnHit -= ScoreUpdate;
        Player.OnDeath -= GameOverUI;
    }
    void Start()
    {
        StartCoroutine(UIInfoUpdate());
    }

    void ScoreUpdate()
    {
        score += 10;
    }
    IEnumerator UIInfoUpdate()
    {
        while (GameManager.Instance.IsGameActive)
        {
            hpText.text = $"HP: {Player.Instance.Health}";
            scoreText.text = $"Score: {score}";
            yield return null;
        }
    }
    void GameOverUI()
    {
        hpText.text = "";
        scoreText.text = "";
        gameOvText.text = "Game Over";
        finalScoreText.text = $"Score: {score}";
        restartButton.gameObject.SetActive(true);
    }
}

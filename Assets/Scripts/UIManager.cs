using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singletone<UIManager>
{
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gameOvText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] Button restartButton;
    Hashtable locTab;
    int score = 0;
    string hp;
    string sc;

    protected override void Awake()
    {
        if (IsInitialized) DestroyImmediate(gameObject);
        base.Awake();
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
        locTab = LanguageManager.TextTab;
        hp = (string)locTab["hp"];
        sc = (string)locTab["score"]; 
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
            hpText.text = $"{hp}: {Player.Instance.Health}";
            scoreText.text = $"{sc}: {score}";
            yield return null;
        }
    }
    void GameOverUI()
    {
        hpText.text = "";
        scoreText.text = "";
        gameOvText.text = (string)locTab["game ov"];
        finalScoreText.text = $"{sc}: {score}";
        restartButton.gameObject.SetActive(true);
    }
}

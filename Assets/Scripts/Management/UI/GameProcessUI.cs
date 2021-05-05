using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameProcessUI : MonoBehaviour
{
    [SerializeField] GameObject UICanvasPrefab;

    GameObject UICanvas;
    GameObject hpUI;
    GameObject scoreUI;
    GameObject gameOvUI;
    GameObject finalScoreUI;
    GameObject restartButtonUI;
    Button restart;

    TextMeshProUGUI restartButtonText;
    TextMeshProUGUI hpText;
    TextMeshProUGUI scoreText;
    TextMeshProUGUI gameOvText;
    TextMeshProUGUI finalScoreText;

    Hashtable locTab;
    int score = 0;
    string hp;
    string sc;

    private void Awake()
    {
        UICanvas = Instantiate(UICanvasPrefab);
        hpUI = GetUIElement(0);
        scoreUI = GetUIElement(1);
        gameOvUI = GetUIElement(2);
        restartButtonUI = GetUIElement(3);
        finalScoreUI = GetUIElement(4);
        restart = restartButtonUI.GetComponent<Button>();

        restartButtonText = restartButtonUI.GetComponentInChildren<TextMeshProUGUI>();
        hpText = GetText(hpUI);
        scoreText = GetText(scoreUI);
        gameOvText = GetText(gameOvUI);
        finalScoreText = GetText(finalScoreUI);
    }

    void Start()
    {
        locTab = LanguageManager.TextTab;
        restartButtonText.text = (string)locTab["again"];
        hp = (string)locTab["hp"];
        sc = (string)locTab["score"];

        StartCoroutine(UIInfoUpdate());
    }

    private void OnEnable()
    {
        BubbleLogic.OnHit += BubbleLogic_OnHit;
        Player.OnDeath += Player_OnDeath;
    }

    private void OnDisable()
    {
        BubbleLogic.OnHit -= BubbleLogic_OnHit;
        Player.OnDeath -= Player_OnDeath;
    }

    private void Player_OnDeath()
    {
        GameOverUI();
    }

    private void BubbleLogic_OnHit()
    {
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        score += 10;
    }
    IEnumerator UIInfoUpdate()
    {
        while (GameManager.CurrentGameState == GameState.Game && GameManager.Instance.IsGameActive)
        {
            hpText.text = $"{hp}: {Player.Health}";
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
        restartButtonUI.SetActive(true);
        restart.onClick.AddListener(delegate () { GameManager.Instance.RestartGame(); });
    }

    TextMeshProUGUI GetText(GameObject item)
    {
        return item.GetComponent<TextMeshProUGUI>();
    }

    GameObject GetUIElement(int n)
    {
        return UICanvas.transform.GetChild(n).gameObject;
    }
}

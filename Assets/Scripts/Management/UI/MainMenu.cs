using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static event Action OnStartButtonClick;

    [SerializeField] GameObject mainMenuCanvasPrefab;

    GameObject startButton;
    GameObject optionsButton;
    GameObject mainMenuCanvas;
    GameObject optionsMenu;
    GameObject doneButton;

    Hashtable locTab;
    TextMeshProUGUI startButtonText;
    TextMeshProUGUI optionsButtonText;
    TextMeshProUGUI doneButtonText;

    private void Awake()
    {
        mainMenuCanvas = Instantiate(mainMenuCanvasPrefab);

        startButton = GetUIElement(3);
        optionsButton = GetUIElement(4);
        optionsMenu = GetUIElement(5);
        doneButton = optionsMenu.transform.GetChild(2).gameObject;

        startButtonText = GetButtonText(startButton);
        optionsButtonText = GetButtonText(optionsButton);
        doneButtonText = GetButtonText(doneButton);
    }
    void Start()
    {
        startButtonText.text = GetWord("start");
        optionsButtonText.text = GetWord("options");
        doneButtonText.text = GetWord("done");
    }
    void OnEnable()
    {
        LanguageManager.OnLanguageChange += LanguageManager_OnLanguageChange;
    }
    void OnDisable()
    {
        LanguageManager.OnLanguageChange -= LanguageManager_OnLanguageChange;
    }

    private void LanguageManager_OnLanguageChange()
    {
        startButtonText.text = GetWord("start");
        optionsButtonText.text = GetWord("options");
        doneButtonText.text = GetWord("done");
    }

    public void StartButtonClick()
    {
        OnStartButtonClick?.Invoke();
    }

    TextMeshProUGUI GetButtonText(GameObject item)
    {
        return item.GetComponentInChildren<TextMeshProUGUI>();
    }

    GameObject GetUIElement(int n)
    {
        return mainMenuCanvas.transform.GetChild(n).gameObject;
    }

    private void OnDestroy()
    {
        Destroy(mainMenuCanvas);
    }
    string GetWord(string key)
    {
        return LanguageManager.TextTab[key] as string;
    }
}

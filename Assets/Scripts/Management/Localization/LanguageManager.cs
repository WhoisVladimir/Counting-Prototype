using System.Collections;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LanguageManager : Singletone<LanguageManager>
{
    public static event Action OnLanguageChange;
    public static LanguageType CurrentLanguage { get; private set; }
    public static Hashtable TextTab { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Localize();
    }

    public void LanguageChoose(int value)
    {
        CurrentLanguage = (LanguageType)value;
        Debug.Log(CurrentLanguage);
        Localize();
        OnLanguageChange?.Invoke();
    }

    void Localize()
    {
        TextTab = new Hashtable();
        XDocument doc = XDocument.Load(@".\Assets\Scripts\Management\Localization\Translation.xml");
        var n = from key in doc.Root.Elements("Key")
                let kn = key
                from translate in kn.Elements("translate").Elements()
                where translate.Name == CurrentLanguage.ToString()
                select (kn.Attribute("Name").Value, translate.Value);

        foreach (var item in n)
        {
            TextTab.Add(item.Item1, item.Item2);
        }
    }

}

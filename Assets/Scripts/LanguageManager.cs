using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }
    public static LanguageType CurrentLanguage { get; private set; }
    //public static Dictionary<string, string> TextDic { get; private set; }
    public static Hashtable TextTab { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        Localize();
    }

    public void LanguageChoose(int value)
    {
        CurrentLanguage = (LanguageType)value;
        Localize();
    }

    void Localize()
    {
        TextTab = new Hashtable();
        XDocument doc = XDocument.Load(@".\Assets\Scripts\Translation.xml");
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

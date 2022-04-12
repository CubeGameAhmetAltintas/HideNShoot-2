//using Newtonsoft.Json;
using UnityEngine;

public class LanguageController : ControllerBaseModel
{
    private static LanguageModel language;

    public override void Initialize()
    {
        base.Initialize();
        DontDestroyOnLoad(this.gameObject);

        Languages Language = Languages.eng;

        switch (Application.systemLanguage)
        {
            case SystemLanguage.Arabic:
                Language = Languages.ara;
                break;
            case SystemLanguage.Chinese:
                Language = Languages.chi;
                break;
            case SystemLanguage.English:
                Language = Languages.eng;
                break;
            case SystemLanguage.French:
                Language = Languages.fre;
                break;
            case SystemLanguage.German:
                Language = Languages.ger;
                break;
            case SystemLanguage.Italian:
                Language = Languages.ita;
                break;
            case SystemLanguage.Japanese:
                Language = Languages.jpn;
                break;
            case SystemLanguage.Korean:
                Language = Languages.kor;
                break;
            case SystemLanguage.Russian:
                Language = Languages.rus;
                break;
            case SystemLanguage.ChineseSimplified:
                Language = Languages.chi;
                break;
            case SystemLanguage.ChineseTraditional:
                Language = Languages.chi;
                break;
            case SystemLanguage.Portuguese:
                Language = Languages.por;
                break;
            default:
                Language = Languages.eng;
                break;
        }

        string strValues = Resources.Load<TextAsset>("Languages/" + Language).text;
        //language = JsonConvert.DeserializeObject<LanguageModel>(strValues);
    }

    public static string Get(string key)
    {
        if (language == null)
        {
            return key;
        }
        else
        {
            return language.Words.Find(x => x.Key == key).Value;
        }
    }

    public static string Get(string key, int number)
    {
        if (language == null)
        {
            return key;
        }
        else
        {
            return language.Words.Find(x => x.Key == key).Value.Replace("(NO)", number.ToString());
        }
    }
    public static string Get(string key, float number)
    {
        if (language == null)
        {
            return key;
        }
        else
        {
            return language.Words.Find(x => x.Key == key).Value.Replace("(NO)", number.ToString());
        }
    }

    public static string Get(string key, long number)
    {
        if (language == null)
        {
            return key;
        }
        else
        {
            return language.Words.Find(x => x.Key == key).Value.Replace("(NO)", number.ToString());
        }
    }

    public static string GetProductName(ProductTypes type)
    {
        switch (type)
        {
            case ProductTypes.Floor:
                return LanguageController.Get("Floor");
            case ProductTypes.Wall:
                return LanguageController.Get("Wall");
            case ProductTypes.Character:
                return LanguageController.Get("Character");
            case ProductTypes.Decorative:
                return LanguageController.Get("Decorative");
            case ProductTypes.Bench:
                return LanguageController.Get("Bench");
            case ProductTypes.Desk:
                return LanguageController.Get("Desk");
            case ProductTypes.Bed:
                return LanguageController.Get("Bed");
            case ProductTypes.Device:
                return LanguageController.Get("Device");
            case ProductTypes.Table:
                return LanguageController.Get("Table");
            case ProductTypes.Counter:
                return LanguageController.Get("Counter");
            case ProductTypes.SpaBed:
                return LanguageController.Get("SpaBed");
            case ProductTypes.ArmChair:
                return LanguageController.Get("ArmChair");
            case ProductTypes.Freezer:
                return LanguageController.Get("Freezer");
            case ProductTypes.LabCounter:
                return LanguageController.Get("LabCounter");
            case ProductTypes.TherapyBed:
                return LanguageController.Get("TherapyBed");
            case ProductTypes.Couch:
                return LanguageController.Get("Couch");
            default:
                return LanguageController.Get("Stuff");
        }
    }

}

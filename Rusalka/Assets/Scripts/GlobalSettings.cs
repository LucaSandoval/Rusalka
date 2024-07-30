using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Language
{
    English, Polish, German, French, Spanish
}
public class GlobalSettings : Singleton<GlobalSettings>
{
    public static Language GlobalLanguage = Language.English;
}

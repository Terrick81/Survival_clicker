using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalizationButton : MonoBehaviour
{
    static int _max;
    public static int current = 1;
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        _max = LocalizationSettings.AvailableLocales.Locales.Count;
        SwithLocale(current);
    }


    public void SwithLocale(int local_current = -1)
    {
        if(local_current == -1)
        {
            LocalizationButton.current++;
        }
        else
        {
            LocalizationButton.current = local_current;
        }
        if (LocalizationButton.current >= _max) { LocalizationButton.current = 0; }
        var locale = LocalizationSettings.AvailableLocales.Locales[LocalizationButton.current];
        LocalizationSettings.SelectedLocale = locale;
    }
}

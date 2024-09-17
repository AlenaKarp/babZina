//this empty line for UTF-8 BOM header
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class ChangeLanguageButton : MonoBehaviour
{
    private bool active = false;

    public void ChangeLocale(int localeID)
    {
        if(active == true)
        {
            return;
        } 

        StartCoroutine(SetLocale(localeID));
    }

    private IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}

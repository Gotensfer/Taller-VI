using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageInitializer : MonoBehaviour
{
    private void Start()
    {
        // Reseteo de datos
        if (PlayerPrefs.GetInt("ResetForFinalRelease", -1) == -1)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("ResetForFinalRelease", 1);         
        }

        if (Application.systemLanguage == SystemLanguage.Spanish)
        {
            ChangeLanguage.Language = 1;
        }
        else
        {
            ChangeLanguage.Language = 0;
        }
    }
}

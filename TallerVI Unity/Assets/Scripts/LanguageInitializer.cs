using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageInitializer : MonoBehaviour
{
    private void Start()
    {
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

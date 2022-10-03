using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoad_SkinDataInitializer : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < Enum.GetValues(typeof(SkinID)).Length; i++)
        {
            if (PlayerPrefs.GetInt($"{Enum.GetName(typeof(SkinID), i)}", -1) == -1)
            {
                // Skin default viene desbloqueada al inicio
                if (i == 0) PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), i)}", 1);

                PlayerPrefs.SetInt($"{Enum.GetName(typeof(SkinID), i)}", 0);
            }
        }

        if (PlayerPrefs.GetInt($"LastSelectedSkin", -1) == -1)
        {
            PlayerPrefs.SetInt($"LastSelectedSkin", 0);
        }
    }
}

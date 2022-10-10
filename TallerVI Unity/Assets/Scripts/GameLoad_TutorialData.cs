using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad_TutorialData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt($"firstTimeMainMenu", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeMainMenu", 1);
        }        

        if (PlayerPrefs.GetInt($"firstTimePreGame", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimePreGame", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeStore", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeStore", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeUpgrades", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeUpgrades", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeAlbum", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeAlbum", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeInGame", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeInGame", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeChili", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeChili", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimeRocket", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimeRocket", 1);
        }

        if (PlayerPrefs.GetInt($"firstTimePidgeon", -1) == -1)
        {
            PlayerPrefs.SetInt($"firstTimePidgeon", 1);
        }

        PlayerPrefs.Save();

    }
}

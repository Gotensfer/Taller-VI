using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoad_TutorialData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

        PlayerPrefs.Save();

    }
}

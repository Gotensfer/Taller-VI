using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class EndGame_Module : MonoBehaviour
{
    [SerializeField] PlayerEvents_Interface playerEvents;
    [SerializeField] DistanceTracker_Module distanceTracker;
    [SerializeField] Rigidbody2D rb;

    [SerializeField] Image fadePanel;
    [SerializeField] FmodEventManager fmod;
    [SerializeField] PlayerStop_Module playerStop;

    bool reachedEndGameFlag = false;
    bool onEndGame = false;

    private void FixedUpdate()
    {

        if (!reachedEndGameFlag)
        {
            if (distanceTracker.travelledDistance >= 200)
            {
                reachedEndGameFlag = true;

                if (PlayerPrefs.GetInt("FirstTimeEndGame", -1) != 1)
                {
                    // Ganar

                    onEndGame = true;

                    
                    fadePanel.DOFade(1, 0.5f);
                    Invoke(nameof(DisableMusic), 0.47f);
                    Invoke(nameof(ToEndGameCinematic), 0.5f);
                    playerStop.bounces = 999; // No será posible perder

                    // Efectos extras aca?

                    // --

                    // PlayerPrefs.SetInt("FirstTimeEndGame", 1);
                }                       
            }
        }    
    }

    void ToEndGameCinematic()
    {
        SceneManager.LoadScene(3);
    }

    private void OnDisable()
    {
        DOTween.Kill(gameObject);
    }

    void DisableMusic()
    {
        fmod.StopMusic();
    }
}

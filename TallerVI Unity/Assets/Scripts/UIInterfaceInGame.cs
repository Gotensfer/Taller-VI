using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System.Net.Sockets;
using UnityEngine.UI;

public class UIInterfaceInGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup launchZonePanel;
    [SerializeField] private RectTransform distance;
    [SerializeField] private RectTransform altitude;
    [SerializeField] private RectTransform velocity;

    [SerializeField] private RectTransform Pause;

    public static bool alreadyInitialized = false;

    private void Start()
    {
        Pause.transform.localScale = Vector3.zero;

        distance.DOAnchorPosX(-37,1);
        altitude.DOAnchorPosX(-1240, 1);
        velocity.DOAnchorPosX(-1240, 1);

        launchZonePanel.DOFade(1, 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }


    public void SetPause()
    {        
        Pause.DOScale(Vector3.one,0).OnComplete(()=> Time.timeScale = 0);
    }

    public void SetResume()
    {
        Time.timeScale =1;
        Pause.DOScale(Vector3.zero, 0.5f);
    }

    public void Reset(int sceneIndex)
    {
        if (sceneIndex < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneIndex);
        alreadyInitialized = true;
        Time.timeScale = 1;
    }

    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }
}

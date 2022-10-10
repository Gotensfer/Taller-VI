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
    [SerializeField] private CanvasGroup launchZonePanel; //Elementos de UI
    [SerializeField] private RectTransform distance;
    [SerializeField] private RectTransform altitude;
    [SerializeField] private RectTransform velocity;
    [SerializeField] private RectTransform Pause;

    [Header("Tutorial check")]
    [SerializeField] private bool firstTimeRocket;
    [SerializeField] private bool firstTimePidgeon;

    [Header("Tutorial Elements")]
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] Button screenButton;
    [SerializeField] RectTransform selector;
    [SerializeField] RectTransform selectorBig;
    [SerializeField] CanvasGroup touchIcon;


    [SerializeField] RectTransform rText1;
    [SerializeField] RectTransform rText2;

    public static bool alreadyInitialized = false;



    private void Start()
    {
        Pause.transform.localScale = Vector3.zero;
        selector.transform.localScale = Vector2.zero;
        selectorBig.transform.localScale = Vector2.zero;
        rText1.transform.localScale = Vector2.zero;
        rText2.transform.localScale = Vector2.zero;

        distance.DOAnchorPosX(-37, 1);
        altitude.DOAnchorPosX(-1240, 1);
        velocity.DOAnchorPosX(-1240, 1);

        launchZonePanel.DOFade(1, 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        /*
        if (PlayerPrefs.GetInt($"firstTimeInGame") == 1) firstTimeInGame = true;
        else firstTimeInGame = false;
        if (PlayerPrefs.GetInt($"firstTimeChili") == 1) firstTimeChili = true;
        else firstTimeChili = false;
        if (PlayerPrefs.GetInt($"firstTimeRocket") == 1) firstTimeRocket = true;
        else firstTimeRocket = false;
        if (PlayerPrefs.GetInt($"firstTimePidgeon") == 1) firstTimePidgeon = true;
        else firstTimePidgeon = false;
        */

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
    public void RocketTutorial()
    {
        if (firstTimeRocket == true)
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 1).SetEase(Ease.OutQuad).SetUpdate(true);
            screenButton.gameObject.SetActive(true);                                //Se activan los elementos necesarios.

            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                rText1.DOScale(Vector2.one, 0.5f).SetUpdate(true);
                rText2.DOScale(Vector2.one, 0.5f).SetUpdate(true);

                touchIcon.DOFade(1, 0.2f)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);

                screenButton.onClick.AddListener(RocketTutorial2);
            }
            );
        }
    }

    private void RocketTutorial2()
    {
        rText1.DOScale(Vector2.zero, 0.5f).SetUpdate(true);
        rText2.DOScale(Vector2.zero, 0.5f).SetUpdate(true);
        screenButton.onClick.RemoveAllListeners();
        touchIcon.gameObject.SetActive(false);
        fadePanel.DOFade(0, 0.3f).SetEase(Ease.InOutBack).SetUpdate(true).OnComplete(() =>
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0.8f).SetEase(Ease.InQuad).SetUpdate(true);
            
            
            screenButton.gameObject.SetActive(false);
        }        
        );
        
    }
}

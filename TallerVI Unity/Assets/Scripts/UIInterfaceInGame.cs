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
    [SerializeField] private bool firstTimeInGame;
    [SerializeField] private bool firstTimeRocket;
    [SerializeField] private bool firstTimePidgeon;
    [SerializeField] int coinsNeededForTutorialToStore;

    [Header("Tutorial Elements")]
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] Button screenButton;
    [SerializeField] RectTransform selector;
    [SerializeField] RectTransform selectorBig;
    [SerializeField] CanvasGroup touchIcon;


    [SerializeField] RectTransform igText1;
    [SerializeField] RectTransform igText2;
    [SerializeField] RectTransform igText3;
    [SerializeField] RectTransform igText4;
    [SerializeField] RectTransform rText1;
    [SerializeField] RectTransform rText2;    
    [SerializeField] RectTransform pText1;
    [SerializeField] RectTransform pText2;    
    [SerializeField] RectTransform sText1;
    [SerializeField] RectTransform sText2;

    public static bool alreadyInitialized = false;



    private void Start()
    {
        Pause.transform.localScale = Vector3.zero;
        selector.transform.localScale = Vector2.zero;
        selectorBig.transform.localScale = Vector2.zero;

        igText1.transform.localScale = Vector2.zero;
        igText2.transform.localScale = Vector2.zero;
        igText3.transform.localScale = Vector2.zero;
        igText4.transform.localScale = Vector2.zero;
        rText1.transform.localScale = Vector2.zero;
        rText2.transform.localScale = Vector2.zero;
        pText1.transform.localScale = Vector2.zero;
        pText2.transform.localScale = Vector2.zero;      
        sText1.transform.localScale = Vector2.zero;
        sText2.transform.localScale = Vector2.zero;

        //distance.DOAnchorPosX(-37, 1);
        altitude.DOAnchorPosX(-1180, 1);
        velocity.DOAnchorPosX(-1080, 1);



        launchZonePanel.DOFade(1, 0.8f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        if (PlayerPrefs.GetInt($"firstTimeInGame") == 1) firstTimeInGame = true;
        else firstTimeInGame = false;
        if (PlayerPrefs.GetInt($"firstTimeRocket") == 1) firstTimeRocket = true;
        else firstTimeRocket = false;
        if (PlayerPrefs.GetInt($"firstTimePidgeon") == 1) firstTimePidgeon = true;
        else firstTimePidgeon = false;
    }

    public void SetPause()
    {
        Pause.gameObject.SetActive(true);
        Pause.DOScale(Vector3.one,0).OnComplete(()=> Time.timeScale = 0);
    }

    public void SetResume()
    {
        Time.timeScale =1;
        Pause.DOScale(Vector3.zero, 0.5f).OnComplete(()=> Pause.gameObject.SetActive(false));
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


    #region"First Time in Game"
    public void InGameSection1()
    {
        if (firstTimeInGame == true)
        {
            screenButton.gameObject.SetActive(true);                                //Se activan los elementos necesarios.
            igText1.gameObject.SetActive(true);
            touchIcon.gameObject.SetActive(true);
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 1).SetEase(Ease.OutQuad).SetUpdate(true); //Se detiene el tiempo con un tweener


            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                igText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetUpdate(true);
                touchIcon.DOFade(1,1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);
                screenButton.onClick.AddListener(InGameSection2);
            }
            );
        }
    }

    public void InGameSection2()
    {
        selector.gameObject.SetActive(true);
        igText2.gameObject.SetActive(true);
        igText3.gameObject.SetActive(true);
        
        igText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {            
            igText1.gameObject.SetActive(false);
            selector.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() =>
            {
                igText3.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetUpdate(true);
                igText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() =>
                screenButton.onClick.AddListener(InGameSection3));
            });
        });

        screenButton.onClick.RemoveListener(InGameSection2);
    }
    public void InGameSection3()
    {
        igText4.gameObject.SetActive(true);

        igText3.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetUpdate(true);
        igText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
        {
            igText2.gameObject.SetActive(false);
            igText4.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() =>
            screenButton.onClick.AddListener(InGameSection4));
        });

        screenButton.onClick.RemoveListener(InGameSection3);
    }    
    public void InGameSection4()
    {
        igText4.DOScale(Vector3.zero, 1).SetEase(Ease.InExpo).SetUpdate(true).OnComplete(() => igText4.gameObject.SetActive(false));
        selector.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => selector.gameObject.SetActive(false));
        screenButton.gameObject.SetActive(false);

        firstTimeInGame = false;
        PlayerPrefs.SetInt($"firstTimeInGame", 0); //Modifica el playerprefs para no volver a ingresar al tuto

        screenButton.onClick.RemoveListener(InGameSection4);
        touchIcon.DOKill();

        fadePanel.DOFade(0, 0.3f).SetEase(Ease.InOutBack).SetUpdate(true).OnComplete(() =>
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0.8f).SetEase(Ease.InQuad).SetUpdate(true);    //Se reactiva el tiempo con un tweener

            screenButton.gameObject.SetActive(false);
            touchIcon.gameObject.SetActive(false);
        }
        );        
    }
#endregion

    #region"Tutorial Rocket"
    public void RocketTutorial()
    {
        if (firstTimeRocket == true)
        {
            rText1.gameObject.SetActive(true);
            rText2.gameObject.SetActive(true);              //Se activan los elementos necesarios.
            screenButton.gameObject.SetActive(true);
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 1).SetEase(Ease.OutQuad).SetUpdate(true); //Se detiene el tiempo con un tweener
                                            

            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                rText1.DOScale(Vector2.one, 0.5f).SetUpdate(true);
                rText2.DOScale(Vector2.one, 0.5f).SetUpdate(true);

                touchIcon.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(1000,-338);
                touchIcon.DOFade(1, 0.2f)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);

                screenButton.onClick.AddListener(RocketTutorial2);
            }
            );
        }

        else
        {
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);

            touchIcon.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(1000, -338);

            touchIcon.DOFade(1, 0.2f)
                    .SetEase(Ease.InQuart)
                    .SetLoops(10, LoopType.Yoyo)
                    .SetUpdate(true)
                    .OnComplete(() => touchIcon.gameObject.SetActive(false));

        }
    }

    private void RocketTutorial2()
    {
        rText1.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(() => rText1.gameObject.SetActive(false));
        rText2.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(() => rText2.gameObject.SetActive(false));
        touchIcon.gameObject.SetActive(false);
        touchIcon.DOKill();

        screenButton.onClick.RemoveListener(RocketTutorial2);

        firstTimeRocket = false;
        PlayerPrefs.SetInt($"firstTimeRocket", 0); //Modifica el playerprefs para no volver a ingresar al tuto

        fadePanel.DOFade(0, 0.3f).SetEase(Ease.InOutBack).SetUpdate(true).OnComplete(() =>
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0.8f).SetEase(Ease.InQuad).SetUpdate(true);//Se reactiva el tiempo con un tweener
            screenButton.gameObject.SetActive(false);
        }        
        );
        
    }
    #endregion.

    #region"Tutorial Pidgeon"
    public void PidgeonTutorial()
    {
        if (firstTimePidgeon == true)
        {
            pText1.gameObject.SetActive(true);
            pText2.gameObject.SetActive(true);      //Se activan los elementos necesarios.
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);
            screenButton.gameObject.SetActive(true);
            
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, 1).SetEase(Ease.OutQuad).SetUpdate(true);  //Se detiene el tiempo con un tweener


            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                pText1.DOScale(Vector2.one, 0.5f).SetUpdate(true);
                pText2.DOScale(Vector2.one, 0.5f).SetUpdate(true);

                touchIcon.DOFade(1, 0.3f).SetEase(Ease.InSine).SetUpdate(true);
                touchIcon.gameObject.GetComponent<RectTransform>().DOAnchorPosY(150, 1)
                    .SetEase(Ease.InOutCubic)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);


                screenButton.onClick.AddListener(PidgeonTutorial2);
            }
            );
        }
        else
        {
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);

            touchIcon.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(1000, -338);

            touchIcon.DOFade(1, 0.3f).SetEase(Ease.InSine).SetUpdate(true);
            touchIcon.gameObject.GetComponent<RectTransform>().DOAnchorPosY(150, 1)
                .SetEase(Ease.InOutCubic)
                .SetLoops(3, LoopType.Yoyo)
                .SetUpdate(true)
                .OnComplete(()=>touchIcon.gameObject.SetActive(false));

        }
    }

    private void PidgeonTutorial2()
    {
        pText1.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(()=> pText1.gameObject.SetActive(false));
        pText2.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(() => pText2.gameObject.SetActive(false));
        touchIcon.DOKill();
        touchIcon.gameObject.SetActive(false);

        firstTimePidgeon = false;
        PlayerPrefs.SetInt($"firstTimePidgeon", 0); //Modifica el playerprefs para no volver a ingresar al tuto

        screenButton.onClick.RemoveListener(PidgeonTutorial2);
        fadePanel.DOFade(0, 0.3f).SetEase(Ease.InOutBack).SetUpdate(true).OnComplete(() =>
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 1, 0.8f).SetEase(Ease.InQuad).SetUpdate(true);    //Se reactiva el tiempo con un tweener

            screenButton.gameObject.SetActive(false);
        }
        );

    }
    #endregion

    #region"Tutorial ToStore"
    public void CheckCoins()
    {
        if (EconomyData.coins >= coinsNeededForTutorialToStore && PlayerPrefs.GetInt("firstTimeUpgrades") == 1)
        {
            sText1.gameObject.SetActive(true);
            sText2.gameObject.SetActive(true);      //Se activan los elementos necesarios.
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);
            screenButton.gameObject.SetActive(true);

            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                sText1.DOScale(Vector2.one, 0.5f).SetUpdate(true);
                sText2.DOScale(Vector2.one, 0.5f).SetUpdate(true);

                // touchIcon.DOFade(1, 0.3f).SetEase(Ease.InSine).SetUpdate(true);
                touchIcon.DOFade(1, 1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);

                screenButton.onClick.AddListener(ToStoreTutorial2);
            }
            );
        }
    }

    private void ToStoreTutorial2()
    {
        sText1.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(() => sText1.gameObject.SetActive(false));
        sText2.DOScale(Vector2.zero, 0.5f).SetUpdate(true).OnComplete(() => sText2.gameObject.SetActive(false));
        touchIcon.DOKill();
        touchIcon.gameObject.SetActive(false);

        screenButton.onClick.RemoveListener(ToStoreTutorial2);
        fadePanel.DOFade(0, 0.3f).SetEase(Ease.InOutBack).SetUpdate(true).OnComplete(() => screenButton.gameObject.SetActive(false));

    }
    #endregion
}

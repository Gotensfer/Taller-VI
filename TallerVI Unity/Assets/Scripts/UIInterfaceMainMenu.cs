using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System.Net.Sockets;
using UnityEngine.UI;

public class UIInterfaceMainMenu : MonoBehaviour
{
    [Header("UI references")] //Referencias de UIs
    [SerializeField] RectTransform mainMenu;
    [SerializeField] RectTransform preGame;
    [SerializeField] RectTransform store;
    [SerializeField] RectTransform upgrades;
    [SerializeField] RectTransform album;
    [SerializeField] RectTransform configuration;
    [SerializeField] RectTransform mainTitle;

    [Header("Polaroid list")]
    public List<RectTransform> polaroids = new List<RectTransform>(); //Lista de polariods

    [Header("Tutorial checks")]
    [SerializeField] bool firstTimeMainMenu; //Flag para entrada al tutorial de cada UI
    [SerializeField] bool firstTimePreGame;
    [SerializeField] bool firstTimeStore;
    [SerializeField] bool firstTimeUpgrades;
    [SerializeField] bool firstTimeAlbum;


    [Header("Tutorial Elements")]
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] Button screenButton;
    [SerializeField] RectTransform screenTransparentButton;
    [SerializeField] RectTransform selector;
    [SerializeField] RectTransform selectorBig;

    [Header("Tutorial Texts")]
    //MainMenu Elements
    [SerializeField] RectTransform mmGreetings;
    [SerializeField] RectTransform mmText1;
    [SerializeField] RectTransform mmText2;

    //PreGame Elements
    [SerializeField] RectTransform pgText1;
    [SerializeField] RectTransform pgText2;
    [SerializeField] RectTransform pgText3;
    [SerializeField] RectTransform pgText4;
    [SerializeField] RectTransform pgText5;
    [SerializeField] RectTransform pgText6;
    [SerializeField] RectTransform pgText7;

    public static bool alreadyInitialized = false;

    private bool isFaded = true;

    //Initialiazing UI with fadeup animation
    private void Start()
    {
        album.transform.localScale = Vector2.zero;
        configuration.transform.localScale = Vector2.zero;
        store.transform.localScale = Vector2.zero;
        upgrades.transform.localScale = Vector2.zero;
        selector.transform.localScale = Vector2.zero;
        selectorBig.transform.localScale = Vector2.zero;
        mmGreetings.transform.localScale = Vector2.zero;
        mmText1.transform.localScale = Vector2.zero;
        mmText2.transform.localScale = Vector2.zero;
        pgText1.transform.localScale = Vector2.zero;
        pgText2.transform.localScale = Vector2.zero;
        pgText3.transform.localScale = Vector2.zero;
        pgText4.transform.localScale = Vector2.zero;
        pgText5.transform.localScale = Vector2.zero;
        pgText6.transform.localScale = Vector2.zero;
        pgText7.transform.localScale = Vector2.zero;


        screenButton.gameObject.SetActive(false);
        screenTransparentButton.gameObject.SetActive(false);

        foreach (RectTransform transform in polaroids)
        {
            transform.transform.localScale = Vector2.zero;
        }

        DOTween.Init();

        // Inicialización distinta si no es la primera vez que se inicializa
        if (alreadyInitialized)
        {
            PlayUIButton();
        }
        else
        {
            mainTitle.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        Fader();

        
        //Se obtienen y se inicializan la variables para tutoriales
        if (PlayerPrefs.GetInt($"firstTimeMainMenu") == 1) firstTimeMainMenu = true;
        else firstTimeMainMenu = false;        
        if (PlayerPrefs.GetInt($"firstTimePreGame") == 1) firstTimePreGame = true;
        else firstTimePreGame = false;
        if (PlayerPrefs.GetInt($"firstTimeStore") == 1) firstTimeStore = true;
        else firstTimeStore = false;
        if (PlayerPrefs.GetInt($"firstTimeUpgrades") == 1) firstTimeUpgrades = true;
        else firstTimeUpgrades = false;
        if (PlayerPrefs.GetInt($"firstTimeAlbum") == 1) firstTimeAlbum = true;
        else firstTimeAlbum = false;
        

        if (firstTimeMainMenu == true)
        {
            firstTimeMainMenu = false;

            screenButton.gameObject.SetActive(true);
            screenTransparentButton.gameObject.SetActive(true);
            screenButton.onClick.AddListener(MainMenuSection1);

        }
        else
        {
            screenButton.gameObject.SetActive(false);
            screenTransparentButton.gameObject.SetActive(false);

        }





    }



    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }

    #region"Animaciones de botones en UI"

    //Play Button
    public void PlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2920, 0), 1f);
        preGame.DOAnchorPos(new Vector2(0, 0), 1);

        if (firstTimePreGame == true)
        {
            firstTimePreGame= false;

            screenButton.gameObject.SetActive(true);
            screenButton.onClick.AddListener(PreGameSection1);

        }
    }
    public void BackFromPlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);

        mainTitle.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        preGame.DOAnchorPos(new Vector2(2920, 0), 1);
    }
    public void PlayFromUpgradeUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
        upgrades.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }

    public void PlayFromAlbumUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
        album.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }

    //Store Button
    public void StoreUIButton()
    {
        store.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        store.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);
    }
    public void BackFromStoreUIButton()
    {
        store.DOAnchorPos(new Vector2(1132, 144), 1).SetEase(Ease.InExpo);
        store.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }
    //Upgrade Button
    public void UpgradesUIButton()
    {
        upgrades.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        upgrades.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);
    }
    public void BackFromUpgradesUIButton()
    {
        upgrades.DOAnchorPos(new Vector2(1090, 0), 1).SetEase(Ease.InExpo);
        upgrades.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }
    public void UpgradeFromPlayUIButton()
    {
        upgrades.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        upgrades.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);
    }

    //Album Button
    public void AlbumUIButton()
    {
        album.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        album.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);
    }
    public void BackFromAlbumUIButton()
    {
        album.DOAnchorPos(new Vector2(1132, -167), 1).SetEase(Ease.InExpo);
        album.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }

    //Config Button
    public void ConfigUIButton()
    {
        configuration.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        configuration.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);

    }
    public void BackFromConfigUIButton()
    {
        configuration.DOAnchorPos(new Vector2(927, -315), 1).SetEase(Ease.InExpo);
        configuration.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }
    #endregion

    #region"Polaroids"
    //Activar Polaroids
    public void Polaroid1On()
    {
        polaroids.ElementAt(0).DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
    }
    public void Polaroid2On()
    {
        polaroids.ElementAt(1).DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
    }
    public void Polaroid3On()
    {
        polaroids.ElementAt(2).DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
    }
    public void Polaroid4On()
    {
        polaroids.ElementAt(3).DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
    }
    public void Polaroid5On()
    {
        polaroids.ElementAt(4).DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
    }

    //Polaroids deactivate
    public void PolaroidClosed()
    {
        foreach (RectTransform transform in polaroids)
        {
            transform.DOComplete();
            transform.DOKill();
            transform.transform.localScale = Vector2.zero;
        }
    }
    #endregion


    //Add-on FADER
    public void Fader()
    {
        isFaded = !isFaded;
        if (isFaded)
            fadePanel.DOFade(1, 3.5f);
        else
            fadePanel.DOFade(0, 2);
    }

    #region"Secciones del tutorial MainMenu"
    private void MainMenuSection1()
    {
        fadePanel.DOFade(0.8f, 1);
        mmGreetings.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
        mmText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        
        screenButton.onClick.AddListener(MainMenuSection2);
        screenButton.onClick.RemoveListener(MainMenuSection1);
    }
    private void MainMenuSection2()
    {

        mmGreetings.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetDelay(0.5f);
        mmText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetDelay(0.5f);


        mmText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selector.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1.7f);

        screenTransparentButton.DOAnchorPos(new Vector2(900, -190), 1);

        screenButton.onClick.AddListener(MainMenuSection3);
        screenButton.onClick.RemoveListener(MainMenuSection2);
    }
    private void MainMenuSection3()
    {
        mmText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        mmText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        selector.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        screenButton.gameObject.SetActive(false);
        screenTransparentButton.gameObject.SetActive(false);

        fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack).WaitForCompletion();

        screenButton.onClick.RemoveListener(MainMenuSection3);
    }
    #endregion


    #region"Secciones del tutorial Pregame (Selección de comidas)"
    private void PreGameSection1()
    {

        fadePanel.DOFade(0.8f, 1);
        pgText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);

        screenButton.onClick.AddListener(PreGameSection2);
        screenButton.onClick.RemoveListener(PreGameSection1);

    }
    private void PreGameSection2()
    {
        pgText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);

        pgText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);

        screenButton.onClick.AddListener(PreGameSection3);
        screenButton.onClick.RemoveListener(PreGameSection2);
    }
    private void PreGameSection3()
    {
        pgText2.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack);

        pgText3.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selectorBig.DOScale(new Vector3(4.7f, 4.1f, 1), 1).SetEase(Ease.OutSine);

        screenButton.onClick.AddListener(PreGameSection4);
        screenButton.onClick.RemoveListener(PreGameSection3);
    }

    private void PreGameSection4()
    {
        pgText3.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack);

        pgText4.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selectorBig.DOAnchorPos(new Vector2(-81, 54), 1);
        selectorBig.DOScaleX(6.24f, 1);

        screenButton.onClick.AddListener(PreGameSection5);
        screenButton.onClick.RemoveListener(PreGameSection4);
    }

    private void PreGameSection5()
    {
        pgText4.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack);

        pgText5.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selectorBig.DOAnchorPos(new Vector2(547, 54), 1);
        selectorBig.DOScaleX(3.8f, 1);

        screenButton.onClick.AddListener(PreGameSection6);
        screenButton.onClick.RemoveListener(PreGameSection5);
    }
    private void PreGameSection6()
    {
        pgText5.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack);

        pgText6.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selectorBig.DOAnchorPos(new Vector2(1088, 62), 1);
        selectorBig.DOScale(new Vector3(5.52f, 3.79f, 1), 1);

        screenButton.onClick.AddListener(PreGameSection7);
        screenButton.onClick.RemoveListener(PreGameSection6);
    }
    private void PreGameSection7()
    {
        pgText6.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack);

        pgText7.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selectorBig.gameObject.SetActive(false);
        selector.DOAnchorPos(new Vector2(1119, -528), 0);
        selector.DOScale(new Vector3(1.44f, 0.63f, 1), 1);

        screenButton.onClick.AddListener(PreGameSection8);
        screenButton.onClick.RemoveListener(PreGameSection7);
    }
    private void PreGameSection8()
    {
        pgText7.DOScale(Vector3.zero, 1).SetEase(Ease.InExpo);
        selector.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
        screenButton.gameObject.SetActive(false);
        screenTransparentButton.gameObject.SetActive(false);

        fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack);

        screenButton.onClick.RemoveListener(PreGameSection8);
    }

    #endregion
}

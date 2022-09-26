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
    [SerializeField] RectTransform selector;

    [Header("Polaroid list")]
    public List<RectTransform> polaroids = new List<RectTransform>(); //Lista de polariods

    [Header("Tutorial checks")]
    [SerializeField]  bool firstTimePreGame; //Flag for enter the tutorial for each window
    [SerializeField]  bool firstTimeStore;
    [SerializeField]  bool firstTimeUpgrades;
    [SerializeField]  bool firstTimeAlbum;

    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] Button screenButton;
    [SerializeField] Button screenTransparentButton;

    [Header("Tutorial Texts")]
    [SerializeField] RectTransform greetings;

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
        greetings.transform.localScale = Vector2.zero;
        screenButton.gameObject.SetActive(true);
        screenTransparentButton.gameObject.SetActive(true);

        foreach (RectTransform transform in polaroids)
        {
            transform.transform.localScale = Vector2.zero;
        }



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

        //Se obtienen y se inicializan la variables para tutoriales
        if (PlayerPrefs.GetInt($"firstTimePreGame") == 1) firstTimePreGame = true;
        else firstTimePreGame = false;
        if (PlayerPrefs.GetInt($"firstTimeStore") == 1) firstTimeStore = true;
        else firstTimeStore = false;
        if (PlayerPrefs.GetInt($"firstTimeUpgrades") == 1) firstTimeUpgrades = true;
        else firstTimeUpgrades = false;
        if (PlayerPrefs.GetInt($"firstTimeAlbum") == 1) firstTimeAlbum = true;
        else firstTimeAlbum = false;

        if (firstTimePreGame == true)
        {
            screenButton.gameObject.SetActive(true);
            screenTransparentButton.gameObject.SetActive(true);
            firstTimePreGame = false;
            screenButton.onClick.AddListener(PregameSections);

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

    #region"UI buttons animations"

    //Play Button
    public void PlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2920, 0), 1f);
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
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
    //Polaroids activate
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



    private void PregameSections()
    {
        print("Empieza tuto Pregame");
        screenButton.onClick.AddListener(StoreSections);
        screenButton.onClick.RemoveListener(PregameSections);
    }
    private void StoreSections()
    {
        print("Empieza tuto Store");
        screenButton.onClick.AddListener(UpdateSections);
        screenButton.onClick.RemoveListener(StoreSections);
    }
    private void UpdateSections()
    {
        print("Empieza tuto Update");
        screenButton.onClick.AddListener(AlbumSections);
        screenButton.onClick.RemoveListener(UpdateSections);
    }
    private void AlbumSections()
    {
        print("Empieza tuto Album");
        screenButton.gameObject.SetActive(false);

    }


}

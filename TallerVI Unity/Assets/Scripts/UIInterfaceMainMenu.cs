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
    [SerializeField] RectTransform store, storeContent; //Skins
    [SerializeField] RectTransform upgrades; //Powerups
    [SerializeField] RectTransform album, albumContent;
    [SerializeField] RectTransform configuration;
    [SerializeField] RectTransform mainTitle;

    [Header("Polaroid list")]
    public List<RectTransform> polaroids = new List<RectTransform>(); //Lista de polariods

    [Header("Tutorial checks")]
    [SerializeField] bool firstTimeMainMenu; //Flag para entrada al tutorial de cada UI
    [SerializeField] bool firstTimePreGame;
    [SerializeField] bool firstTimeStore; //Skins
    [SerializeField] bool firstTimeUpgrades; //Powerups
    [SerializeField] bool firstTimeAlbum;


    [Header("Tutorial Elements")]
    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] CanvasGroup touchIcon;
    [SerializeField] Button screenButton;
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

    //Store Elements
    [SerializeField] RectTransform sText1;
    [SerializeField] RectTransform sText2;

    //Upgrades Elements
    [SerializeField] RectTransform uText1;
    [SerializeField] RectTransform uText2;

    //Album Elements
    [SerializeField] RectTransform aText1;
    [SerializeField] RectTransform aText2;

    [Header("Store Descriptions")]
    [SerializeField] RectTransform desText1;
    [SerializeField] RectTransform desText2;
    [SerializeField] RectTransform desText3;
    [SerializeField] RectTransform desText4;
    [SerializeField] RectTransform desText5;


    public static bool alreadyInitialized = false;

    private bool isFaded = true;


    private void Start()
    {
        
        #region "Elementos de UI escala 0"

        album.transform.localScale = Vector2.zero;
        configuration.transform.localScale = Vector2.zero;
        store.transform.localScale = Vector2.zero;
        upgrades.transform.localScale = Vector2.zero;

        //Escala 0
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
        sText1.transform.localScale = Vector2.zero;
        sText2.transform.localScale = Vector2.zero;
        uText1.transform.localScale = Vector2.zero;
        uText2.transform.localScale = Vector2.zero;
        aText1.transform.localScale = Vector2.zero;
        aText2.transform.localScale = Vector2.zero;

        desText1.transform.localScale = Vector2.zero;
        desText2.transform.localScale = Vector2.zero;
        desText3.transform.localScale = Vector2.zero;
        desText4.transform.localScale = Vector2.zero;
        desText5.transform.localScale = Vector2.zero;

        albumContent.gameObject.SetActive(false);
        storeContent.gameObject.SetActive(false);

        #endregion

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

        Fader(); //Fade al inicio del juego

        
        //Se obtienen y se inicializan las variables para tutoriales
        if (PlayerPrefs.GetInt($"firstTimeMainMenu") == 1) firstTimeMainMenu = false; //Se elimina la bienvenida.
        else firstTimeMainMenu = false;        
        if (PlayerPrefs.GetInt($"firstTimePreGame") == 1) firstTimePreGame = true;
        else firstTimePreGame = false;
        if (PlayerPrefs.GetInt($"firstTimeStore") == 1) firstTimeStore = true;
        else firstTimeStore = false;
        if (PlayerPrefs.GetInt($"firstTimeUpgrades") == 1) firstTimeUpgrades = true;
        else firstTimeUpgrades = false;
        if (PlayerPrefs.GetInt($"firstTimeAlbum") == 1) firstTimeAlbum = true;
        else firstTimeAlbum = false;


        //Empieza el tutorial
        if (firstTimeMainMenu == true)
        {
            screenButton.gameObject.SetActive(true);                                //Se activan los elementos necesarios.
            mmGreetings.gameObject.SetActive(true);
            mmText1.gameObject.SetActive(true);

            fadePanel.DOFade(0.8f, 1).OnComplete(() => {                                        
            mmGreetings.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);              //Se escalan los elementos
            mmText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1).OnComplete(() => 
            screenButton.onClick.AddListener(MainMenuSection2));
        }).SetDelay(2); //Delay despues de inicio
        }
        else
        {
            screenButton.gameObject.SetActive(false);

        }
    }



    private void OnDisable() //Cierre de tweeners
    {
        DOTween.KillAll(gameObject);
    }


    //Add-on FADER
    public void Fader()
    {
        isFaded = !isFaded;
        if (isFaded)
            fadePanel.DOFade(1, 3.5f);
        else
            fadePanel.DOFade(0, 2);
    }


    #region"Animaciones de botones en UI"

    //Play Button
    public void PlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2920, 0), 1f);
        preGame.DOAnchorPos(new Vector2(0, 0), 1);

        //Primera Seccion tutorial comidas
        if (firstTimePreGame == true)
        {
            screenButton.gameObject.SetActive(true);
            pgText1.gameObject.SetActive(true);
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);

            touchIcon.DOFade(1, 1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);

            fadePanel.DOFade(0.8f, 1).OnComplete(() =>
            pgText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() =>screenButton.onClick.AddListener(PreGameSection2)));
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
        store.gameObject.SetActive(true);
        storeContent.gameObject.SetActive(true);
        
        store.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        store.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);

        //Primera Seccion tutorial store
        if (firstTimeStore == true)
        {
            screenButton.gameObject.SetActive(true);
            sText1.gameObject.SetActive(true);
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);
            

            fadePanel.DOFade(0.8f, 1).OnComplete(() =>
            {
                touchIcon.DOFade(1, 1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);
                sText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() => screenButton.onClick.AddListener(StoreSection2));                
            }
            );
        }
    }
    public void BackFromStoreUIButton()
    {
        store.DOAnchorPos(new Vector2(1132, 144), 1).SetEase(Ease.InExpo);
        store.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(()=> {
            store.gameObject.SetActive(false);
            storeContent.gameObject.SetActive(false);
        });
    }

    #region "Descripción Chili"
    public void ChiliDescription()
    {
        desText1.gameObject.SetActive(true);
        desText1.DOScale(Vector3.one, 0.5f);
    }

    public void CloseChiliDescription()
    {
        desText1.DOScale(Vector3.zero, 0.5f).OnComplete(() => desText1.gameObject.SetActive(false));
    }
    #endregion

    #region "Descripción Rocket"
    public void RocketDescription()
    {
        desText2.gameObject.SetActive(true);
        desText2.DOScale(Vector3.one, 0.5f);
    }

    public void CloseRocketDescription()
    {
        desText2.DOScale(Vector3.zero, 0.5f).OnComplete(() => desText2.gameObject.SetActive(false));
    }

    #endregion

    #region "Descripción Pidgeon"

    public void PidgeonDescription()
    {
        desText3.gameObject.SetActive(true);
        desText3.DOScale(Vector3.one, 0.5f);
    }

    public void ClosePidgeonDescription()
    {
        desText3.DOScale(Vector3.zero, 0.5f).OnComplete(() => desText3.gameObject.SetActive(false));
    }

    #endregion

    #region "Descripción Mitosis"

    public void MitosisDescription()
    {
        desText4.gameObject.SetActive(true);
        desText4.DOScale(Vector3.one, 0.5f);
    }

    public void CloseMitosisDescription()
    {
        desText4.DOScale(Vector3.zero, 0.5f).OnComplete(() => desText4.gameObject.SetActive(false));
    }

    #endregion

    #region "Descripción Fecalito"

    public void FecalitoDescription()
    {
        desText5.gameObject.SetActive(true);
        desText5.DOScale(Vector3.one, 0.5f);
    }

    public void CloseFecalitoDescription()
    {
        desText5.DOScale(Vector3.zero, 0.5f).OnComplete(() => desText5.gameObject.SetActive(false));
    }

    #endregion

    //Upgrade Button
    public void UpgradesUIButton()
    {
        upgrades.gameObject.SetActive(true);
        upgrades.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        upgrades.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);

        //Primera Seccion tutorial upgrades
        if (firstTimeUpgrades == true)
        {
            screenButton.gameObject.SetActive(true);
            uText1.gameObject.SetActive(true);
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);


            fadePanel.DOFade(0.8f, 1).OnComplete(() =>
            {
                touchIcon.DOFade(1, 1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);
                uText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() => screenButton.onClick.AddListener(UpgradesSection2));
            }
            );
        }
    }
    public void BackFromUpgradesUIButton()
    {
        upgrades.DOAnchorPos(new Vector2(1090, 0), 1).SetEase(Ease.InExpo);
        upgrades.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => upgrades.gameObject.SetActive(false));
    }
    public void UpgradeFromPlayUIButton()
    {
        upgrades.gameObject.SetActive(true);
        upgrades.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        upgrades.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);
    }

    //Album Button
    public void AlbumUIButton()
    {
        album.gameObject.SetActive(true);
        albumContent.gameObject.SetActive(true);
        album.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        album.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);

        //Primera Seccion tutorial store
        if (firstTimeAlbum == true)
        {
            screenButton.gameObject.SetActive(true);
            aText1.gameObject.SetActive(true);
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);


            fadePanel.DOFade(0.8f, 1).OnComplete(() =>
            {
                touchIcon.DOFade(1, 1)
                    .SetEase(Ease.InQuart)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);
                aText1.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() => screenButton.onClick.AddListener(AlbumSection2));
            }
            );
        }
    }
    public void BackFromAlbumUIButton()
    {
        album.DOAnchorPos(new Vector2(1132, -167), 1).SetEase(Ease.InExpo);
        album.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            album.gameObject.SetActive(false);
            albumContent.gameObject.SetActive(false);
        });
    }

    //Config Button
    public void ConfigUIButton()
    {
        configuration.gameObject.SetActive(true);
        configuration.DOAnchorPos(new Vector2(0, 0), 0.8f).SetEase(Ease.OutExpo);
        configuration.DOScale(Vector3.one, 0.8f).SetEase(Ease.OutExpo);

    }
    public void BackFromConfigUIButton()
    {
        configuration.DOAnchorPos(new Vector2(927, -315), 1).SetEase(Ease.InExpo);
        configuration.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => configuration.gameObject.SetActive(false));
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

    #region"Secciones del tutorial MainMenu"
    
    //Primera sección del tutorial en Start()
    
    private void MainMenuSection2()
    {
        mmGreetings.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(()=> mmGreetings.gameObject.SetActive(false)); //Al terminar el tweener se desactivan de nuevo
        mmText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).SetDelay(0.5f).OnComplete(() => mmText1.gameObject.SetActive(false));
        mmText2.gameObject.SetActive(true);
        selector.gameObject.SetActive(true);

        mmText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1);
        selector.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).SetDelay(1.7f).OnComplete(() => screenButton.onClick.AddListener(MainMenuSection3));


        screenButton.onClick.RemoveListener(MainMenuSection2);
    }
    private void MainMenuSection3()
    {
        mmText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => mmText2.gameObject.SetActive(false));
        selector.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => selector.gameObject.SetActive(false));


        screenButton.gameObject.SetActive(false);

        fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack).OnComplete(()=>PlayUIButton());

        screenButton.onClick.RemoveListener(MainMenuSection3);

        firstTimeMainMenu = false;
        PlayerPrefs.SetInt($"firstTimeMainMenu", 0); //Modifica el playerprefs para no volver a ingresar al tuto
    }
    #endregion

    #region"Secciones del tutorial Pregame (Selección de comidas)"

    //Primera sección del tutorial en el botón de play
    private void PreGameSection2()
    {
        pgText2.gameObject.SetActive(true);

        pgText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() =>
        {    
            pgText1.gameObject.SetActive(false);
            pgText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() =>
            screenButton.onClick.AddListener(PreGameSection3));
        });              

        screenButton.onClick.RemoveListener(PreGameSection2);
    }
    private void PreGameSection3()
    {
        pgText3.gameObject.SetActive(true);
        selectorBig.gameObject.SetActive(true);

        pgText2.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(() =>
        {
            pgText2.gameObject.SetActive(false);

            pgText3.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
            selectorBig.DOScale(new Vector3(4.7f, 4.1f, 1), 1).SetEase(Ease.OutSine).OnComplete(() =>
            screenButton.onClick.AddListener(PreGameSection4));
        });               

        screenButton.onClick.RemoveListener(PreGameSection3);
    }

    private void PreGameSection4()
    {
        pgText4.gameObject.SetActive(true);

        pgText3.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(() =>
        {
            pgText3.gameObject.SetActive(false);

            pgText4.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
            selectorBig.DOAnchorPos(new Vector2(-81, 54), 1);
            selectorBig.DOScaleX(6.24f, 1).OnComplete(() => screenButton.onClick.AddListener(PreGameSection5));
        });

        screenButton.onClick.RemoveListener(PreGameSection4);
    }

    private void PreGameSection5()
    {
        pgText5.gameObject.SetActive(true);

        pgText4.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(() =>
        {
            pgText4.gameObject.SetActive(false);

            pgText5.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
            selectorBig.DOAnchorPos(new Vector2(547, 54), 1);
            selectorBig.DOScaleX(3.8f, 1).OnComplete(() => screenButton.onClick.AddListener(PreGameSection6));
        });

        screenButton.onClick.RemoveListener(PreGameSection5);
    }
    private void PreGameSection6()
    {
        pgText6.gameObject.SetActive(true);

        pgText5.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(() => 
        {
            pgText5.gameObject.SetActive(false);
            pgText6.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
            selectorBig.DOAnchorPos(new Vector2(1088, 62), 1);
            selectorBig.DOScale(new Vector3(5.52f, 3.79f, 1), 1).OnComplete(() => screenButton.onClick.AddListener(PreGameSection7));
        });

        screenButton.onClick.RemoveListener(PreGameSection6);
    }
    private void PreGameSection7()
    {
        pgText7.gameObject.SetActive(true);

        pgText6.DOScale(Vector3.zero, 0.7f).SetEase(Ease.InBack).OnComplete(() =>
        {
            pgText6.gameObject.SetActive(false);
            touchIcon.DOKill();            
            touchIcon.gameObject.SetActive(false);

            pgText7.DOScale(Vector3.one, 1).SetEase(Ease.OutSine);
            selectorBig.gameObject.SetActive(false);
            selector.DOAnchorPos(new Vector2(1119, -528), 0);
            selector.DOScale(new Vector3(1.44f, 0.63f, 1), 1).OnComplete(() => screenButton.onClick.AddListener(PreGameSection8));
        });
        
        screenButton.onClick.RemoveListener(PreGameSection7);
    }
    private void PreGameSection8()
    {
        pgText7.DOScale(Vector3.zero, 1).SetEase(Ease.InExpo).OnComplete(() => pgText7.gameObject.SetActive(false));
        selector.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => selector.gameObject.SetActive(false));
        screenButton.gameObject.SetActive(false);

        fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack);

        screenButton.onClick.RemoveListener(PreGameSection8);


        firstTimePreGame = false;
        PlayerPrefs.SetInt($"firstTimePreGame", 0); //Modifica el playerprefs para no volver a ingresar al tuto
    }

    #endregion

    #region"Secciones del tutorial Store"

    //Primera sección del tutorial en Start()

    private void StoreSection2()
    {
        screenButton.onClick.RemoveListener(StoreSection2);
        sText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            sText1.gameObject.SetActive(false);
            sText2.gameObject.SetActive(true);

            sText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() =>screenButton.onClick.AddListener(StoreSection3));
        });
    }
    private void StoreSection3()
    {
        screenButton.onClick.RemoveListener(StoreSection3);
        sText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            sText2.gameObject.SetActive(false);
            screenButton.gameObject.SetActive(false);

            fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack);

            touchIcon.DOKill();
            touchIcon.gameObject.SetActive(false);

            firstTimeStore = false;
            PlayerPrefs.SetInt($"firstTimeStore", 0); //Modifica el playerprefs para no volver a ingresar al tuto
        });

    }
    #endregion

    #region"Secciones del tutorial Upgrades"

    //Primera sección del tutorial en Start()

    private void UpgradesSection2()
    {
        screenButton.onClick.RemoveListener(UpgradesSection2);
        uText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            uText1.gameObject.SetActive(false);
            uText2.gameObject.SetActive(true);

            uText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() => screenButton.onClick.AddListener(UpgradesSection3));
        });
    }
    private void UpgradesSection3()
    {
        screenButton.onClick.RemoveListener(UpgradesSection3);
        uText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            uText2.gameObject.SetActive(false);
            screenButton.gameObject.SetActive(false);

            fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack);

            touchIcon.DOKill();
            touchIcon.gameObject.SetActive(false);

            firstTimeUpgrades = false;
            PlayerPrefs.SetInt($"firstTimeUpgrades", 0); //Modifica el playerprefs para no volver a ingresar al tuto
        });

    }
    #endregion

    #region"Secciones del tutorial Album"

    //Primera sección del tutorial en Start()

    private void AlbumSection2()
    {
        screenButton.onClick.RemoveListener(AlbumSection2);
        aText1.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            aText1.gameObject.SetActive(false);
            aText2.gameObject.SetActive(true);

            aText2.DOScale(Vector3.one, 1).SetEase(Ease.OutSine).OnComplete(() => screenButton.onClick.AddListener(AlbumSection3));
        });
    }
    private void AlbumSection3()
    {
        screenButton.onClick.RemoveListener(AlbumSection3);
        aText2.DOScale(Vector3.zero, 1).SetEase(Ease.InBack).OnComplete(() => {
            aText2.gameObject.SetActive(false);
            screenButton.gameObject.SetActive(false);

            fadePanel.DOFade(0, 1).SetEase(Ease.InOutBack);

            touchIcon.DOKill();
            touchIcon.gameObject.SetActive(false);

            firstTimeAlbum = false;
            PlayerPrefs.SetInt($"firstTimeAlbum", 0); //Modifica el playerprefs para no volver a ingresar al tuto
        });

    }
    #endregion
}

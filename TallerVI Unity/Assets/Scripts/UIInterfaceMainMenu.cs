using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System.Net.Sockets;

public class UIInterfaceMainMenu : MonoBehaviour
{
    [SerializeField] private bool firstTimePreGame, firstTimeStore, firstTimeUpgrades, firstTimeAlbum; //Flag for enter the tutorial for each window

    public RectTransform mainMenu, preGame, store, upgrades, album, configuration, mainTitle; //References for UI position
    public List<RectTransform> polaroids = new List<RectTransform>(); //List of polaroids
    public CanvasGroup fadePanel;

    public static bool alreadyInitialized = false;

    private bool isFaded = true;

    //Initialiazing UI with fadeup animation
    private void Start()
    {
        album.transform.localScale = Vector2.zero;
        configuration.transform.localScale = Vector2.zero;
        store.transform.localScale = Vector2.zero;
        upgrades.transform.localScale = Vector2.zero;

        foreach (RectTransform transform in polaroids)
        {
            transform.transform.localScale = Vector2.zero;
        }

        Fader();


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
    }

    public void Reset(int sceneIndex)
    {
        if (sceneIndex < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneIndex);
        alreadyInitialized = true;
    }


    //Slide for UI buttons and animations (DOTween)

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


    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }

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

    //Add-on FADER
    public void Fader()
    {
        isFaded = !isFaded;
        if (isFaded)
            fadePanel.DOFade(1, 3.5f);
        else
            fadePanel.DOFade(0, 2);
    }

    public void FirstTimeInPreGame()
    {
        if (firstTimePreGame == true)
        {
            fadePanel.DOFade(0.6f, 1);
        }
    }

}

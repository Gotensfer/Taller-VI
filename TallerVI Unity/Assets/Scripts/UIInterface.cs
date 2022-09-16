using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIInterface : MonoBehaviour
{
    public RectTransform mainMenu, preGame,store, upgrades, album,configuration, mainTitle; //References for UI position

    public static bool alreadyInitialized = false;

    //Initialiazing UI with fadeup animation
    private void Start()
    {        
        album.transform.localScale = Vector2.zero;
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
        preGame.DOAnchorPos(new Vector2(2920, 0), 1);
    }
    public void PlayFromUpgradeUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
        upgrades.DOAnchorPos(new Vector2(0, -1700), 1);
    }
    
    //Store Button
    public void StoreUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2920, 0), 1);
        store.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromStoreUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        store.DOAnchorPos(new Vector2(0, -1700), 1);
    }
    //Upgrade Button
    public void UpgradesUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-2920, 0), 1);
        upgrades.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromUpgradesUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        upgrades.DOAnchorPos(new Vector2(0, -1700), 1);
    }
    public void UpgradeFromPlayUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 1700), 1);
        upgrades.DOAnchorPos(new Vector2(0, 0), 1);
    }

    //Album Button
    public void AlbumUIButton()
    {
        album.DOScale(Vector3.one, 0.8f);
    }
    public void BackFromAlbumUIButton()
    {
        album.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }

    //Config Button
    public void ConfigUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(2920, 0), 1);
        configuration.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromConfigUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        configuration.DOAnchorPos(new Vector2(-2920, 0), 1);
    }

    public void PlayFromAlbumUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
        album.DOScale(Vector3.zero, 1).SetEase(Ease.InBack);
    }
    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }
}

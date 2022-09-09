using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIInterface : MonoBehaviour
{
    public RectTransform mainMenu, preGame,store, upgrades, album,configuration, mainTitle, background; //References for UI position

    //Initialiazing UI with fadeup animation
    private void Start()
    {        
        mainMenu.DOAnchorPos(Vector2.zero, 2);
        background.DOAnchorPos(new Vector2(-1460, -820), 2);
        mainTitle.DOScale(new Vector3(1,1,1),1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void Reset(int sceneIndex)
    {
        if (sceneIndex < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneIndex);
    }


    //Slide for UI buttons and animations (DOTween)
    
    //Play Button
    public void PlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-3000, 0), 0.5f);
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromPlayUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        preGame.DOAnchorPos(new Vector2(3000, 0), 1);
    }
    public void PlayFromUpgradeUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 0), 1);
        upgrades.DOAnchorPos(new Vector2(0, -1700), 1);
    }
    
    //Store Button
    public void StoreUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(-3000, 0), 1);
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
        mainMenu.DOAnchorPos(new Vector2(-3000, 0), 1);
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
        mainMenu.DOAnchorPos(new Vector2(-3000, 0), 1);
        album.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromAlbumUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        album.DOAnchorPos(new Vector2(0, -1700), 1);
    }

    //Config Button
    public void ConfigUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(3000, 0), 1);
        configuration.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void BackFromConfigUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        configuration.DOAnchorPos(new Vector2(-3000, 0), 1);
    }
    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }
}

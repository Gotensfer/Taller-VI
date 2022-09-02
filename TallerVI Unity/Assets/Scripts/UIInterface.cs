using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIInterface : MonoBehaviour
{
    public RectTransform mainMenu, preGame,store,configuration,upgrades; //References for UI position

    //Initialiazing UI with fadeup animation
    private void Start()
    {
        mainMenu.DOAnchorPos(Vector2.zero, 2);
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
        upgrades.DOAnchorPos(new Vector2(-3000, 0), 1);
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
        store.DOAnchorPos(new Vector2(0, -1600), 1);
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
        upgrades.DOAnchorPos(new Vector2(0, -1600), 1);
    }
    public void UpgradeFromPlayUIButton()
    {
        preGame.DOAnchorPos(new Vector2(0, 1600), 1);
        upgrades.DOAnchorPos(new Vector2(0, 0), 1);
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

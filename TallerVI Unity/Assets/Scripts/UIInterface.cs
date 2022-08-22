using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIInterface : MonoBehaviour
{
    public RectTransform mainMenu, gameUI; //References for UI position

    [SerializeField] int ResetTo;

    //Initialiazing UI with fadeup animation
    private void Start()
    {
        mainMenu.DOAnchorPos(Vector2.zero, 2);
    }
    public void Reset()
    {
        SceneManager.LoadScene(ResetTo);
    }
    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }

    //Slide for UI buttons and animations (DOTween)
    public void playUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, -1600), 0.5f);
        gameUI.DOAnchorPos(new Vector2(0, 0), 1);
    }
    public void closeUIButton()
    {
        mainMenu.DOAnchorPos(new Vector2(0, 0), 1);
        gameUI.DOAnchorPos(new Vector2(3000, 0), 1);
    }
}

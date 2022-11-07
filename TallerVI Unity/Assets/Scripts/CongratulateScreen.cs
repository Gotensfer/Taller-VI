using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public class CongratulateScreen : MonoBehaviour
{
    [SerializeField] float timeBetweenMessageChanges;

    [Header("Objetos de texto")]
    [SerializeField] RectTransform firstMsg;
    [SerializeField] RectTransform secondMsg;
    [SerializeField] RectTransform thirdMsg;
    [SerializeField] RectTransform fourthMsg;

    Button continueButton;

    private void Start()
    {
        continueButton = GetComponent<Button>();

        firstMsg.localScale = Vector3.zero;
        secondMsg.localScale = Vector3.zero;
        thirdMsg.localScale = Vector3.zero;
        fourthMsg.localScale = Vector3.zero;

        ShowFirstMessage();
    }

    void ShowFirstMessage()
    {
        TextFadeIn(firstMsg).OnComplete( () => continueButton.onClick.AddListener(ShowSecondMessage) );
    }

    void ShowSecondMessage()
    {
        continueButton.onClick.RemoveListener(ShowSecondMessage);

        TextFadeOut(firstMsg);
        TextFadeIn(secondMsg).OnComplete(() => continueButton.onClick.AddListener(ShowThirdMessage));
    }

    void ShowThirdMessage()
    {
        continueButton.onClick.RemoveListener(ShowThirdMessage);

        TextFadeOut(secondMsg);
        TextFadeIn(thirdMsg).OnComplete(() => continueButton.onClick.AddListener(ShowFourthMessage));
    }

    void ShowFourthMessage()
    {
        continueButton.onClick.RemoveListener(ShowFourthMessage);

        TextFadeOut(thirdMsg);
        TextFadeIn(fourthMsg).OnComplete(() => continueButton.onClick.AddListener(ToMainMenu));
    }

    Tween TextFadeIn(RectTransform text)
    {
        return text.DOScale(Vector3.one, timeBetweenMessageChanges / 2).SetDelay(timeBetweenMessageChanges / 2);
    }

    Tween TextFadeOut(RectTransform text)
    {
        return text.DOScale(Vector3.zero, timeBetweenMessageChanges / 2);
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
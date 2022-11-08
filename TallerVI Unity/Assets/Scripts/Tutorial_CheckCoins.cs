using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_CheckCoins : MonoBehaviour
{
    [SerializeField] int coinsNeededForTutorialToStore;


    [SerializeField] CanvasGroup fadePanel;
    [SerializeField] Button screenButton;
    [SerializeField] CanvasGroup touchIcon;

    [SerializeField] RectTransform sText1;
    [SerializeField] RectTransform sText2;

    // Este método debe estar agregado en CrashEvent
    public void CheckCoins()
    {
        if (EconomyData.coins >= coinsNeededForTutorialToStore && PlayerPrefs.GetInt("firstTimeUpgrades") == 1)
        {
            ToStoreTutorial();
        }
    }

    private void Start()
    {
        sText1.transform.localScale = Vector2.zero;
        sText2.transform.localScale = Vector2.zero;
    }

    public void ToStoreTutorial()
    {
            sText1.gameObject.SetActive(true);
            sText2.gameObject.SetActive(true);      //Se activan los elementos necesarios.
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);
            screenButton.gameObject.SetActive(true);

            fadePanel.DOFade(0.8f, 0.3f).SetUpdate(true).OnComplete(() => {
                sText1.DOScale(Vector2.one, 0.5f).SetUpdate(true);
                sText2.DOScale(Vector2.one, 0.5f).SetUpdate(true);

                touchIcon.DOFade(1, 0.3f).SetEase(Ease.InSine).SetUpdate(true);
                touchIcon.gameObject.GetComponent<RectTransform>().DOAnchorPosY(150, 1)
                    .SetEase(Ease.InOutCubic)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetUpdate(true);


                screenButton.onClick.AddListener(ToStoreTutorial2);
            }
            );
        
            touchIcon.alpha = 0;
            touchIcon.gameObject.SetActive(true);

            touchIcon.gameObject.GetComponent<RectTransform>().localPosition = new Vector2(1000, -338);

            touchIcon.DOFade(1, 0.3f).SetEase(Ease.InSine).SetUpdate(true);
            touchIcon.gameObject.GetComponent<RectTransform>().DOAnchorPosY(150, 1)
                .SetEase(Ease.InOutCubic)
                .SetLoops(3, LoopType.Yoyo)
                .SetUpdate(true)
                .OnComplete(() => touchIcon.gameObject.SetActive(false));
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
}
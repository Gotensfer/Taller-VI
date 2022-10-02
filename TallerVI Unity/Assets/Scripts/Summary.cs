using UnityEngine;
using TMPro;
using DG.Tweening;

public class Summary : MonoBehaviour
{
    private CanvasGroup cnv;
    [SerializeField] RectTransform homeButton;
    [SerializeField] RectTransform restartButton;


    [SerializeField] private TMP_Text travelledDistance, localRecord, money;
    [SerializeField] private GameObject alert;
    private DistanceTracker_Module dT;
    
    // Start is called before the first frame update
    void Start()
    {

        homeButton.transform.localScale = Vector2.zero;
        restartButton.transform.localScale = Vector2.zero;

        cnv = GetComponent<CanvasGroup>();
        dT = FindObjectOfType<DistanceTracker_Module>();
        cnv.alpha = 0;
    }

    private void Update()
    {
        travelledDistance.text = dT.travelledDistance.ToString("0.##") + "m";
        localRecord.text = dT.localRecord.ToString("0.##") + "m";
        //money.text = (lo que sea que lo envie);
    }

    public void DisplaySummary()
    {

        cnv.DOFade(1,1).SetDelay(1);
        homeButton.DOScale(new Vector3(1.621723f, 1.621723f, 1.621723f), 1).SetEase(Ease.OutSine).SetDelay(1);
        restartButton.DOScale(new Vector3(1.621723f, 1.621723f, 1.621723f), 1).SetEase(Ease.OutSine).SetDelay(1);


        if (dT.localRecord < dT.travelledDistance)
        {
            alert.SetActive(true);
        }
    }

    private void OnDisable()
    {
        DOTween.KillAll(gameObject);
    }
}

using UnityEngine;
using TMPro;

public class Summary : MonoBehaviour
{
    private CanvasGroup cnv;
    [SerializeField] private TMP_Text travelledDistance, localRecord, money;
    [SerializeField] private GameObject alert;
    private DistanceTracker_Module dT;
    
    // Start is called before the first frame update
    void Start()
    {
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
        cnv.alpha = 1;

        if (dT.localRecord < dT.travelledDistance)
        {
            alert.SetActive(true);
        }
    }
}
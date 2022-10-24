using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private LeaderBoardElement prefab;
    [SerializeField] private RectTransform content;
    private bool canDisplay = false;

    private void Start()
    {
        RealtimeDatabase.Instance.DataLoaded.AddListener(Change);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DisplayData();
        }
    }
    
    void DisplayData()
    {
        DeleteChild();

        StartCoroutine(Spawn());
    }

    void DeleteChild()
    {
        foreach (RectTransform item in content)
        {
            Destroy(item.gameObject);
        }
    }

    void Change()
    {
        canDisplay = true;
    }

    IEnumerator Spawn()
    {
        RealtimeDatabase.Instance.LoadData();
        
        yield return new WaitUntil(() => canDisplay);
        
        foreach (KeyValuePair<string, float> element in RealtimeDatabase.Instance.playerScores.OrderByDescending(key => key.Value))
        {
            LeaderBoardElement l = Instantiate(prefab, content);
            l.user.text = element.Key;
            l.score.text = element.Value.ToString();
        }

        canDisplay = false;
    }
}

using System;
using UnityEngine;
using TMPro;

public class Localizacion : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private string textSpanish, textEnglish;

    private ChangeLanguage cl;

    private void Awake()
    {
        cl = GameObject.Find("ChangeLanguage").GetComponent<ChangeLanguage>();
    }

    private void Start()
    {
        cl.ChangeEnglish.AddListener(ChangeToEnglish);
        cl.ChangeSpanish.AddListener(ChangeToSpanish);
    }

    void ChangeToSpanish()
    {
        text.text = textSpanish;
    }
    
    void ChangeToEnglish()
    {
        text.text = textEnglish;
    }
}

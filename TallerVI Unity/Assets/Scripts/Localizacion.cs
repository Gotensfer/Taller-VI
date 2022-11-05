using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Localizacion : MonoBehaviour
{
    [SerializeField] private string textSpanish, textEnglish;

    private TMP_Text text;
    private ChangeLanguage cl;

    private void Awake()
    {
        cl = GameObject.Find("ChangeLanguage").GetComponent<ChangeLanguage>();
        text = gameObject.GetComponent<TMP_Text>();
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

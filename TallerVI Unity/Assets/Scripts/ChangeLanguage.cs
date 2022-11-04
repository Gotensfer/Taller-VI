using System;
using UnityEngine;
using UnityEngine.Events;

public class ChangeLanguage : MonoBehaviour
{
    public UnityEvent ChangeSpanish, ChangeEnglish;

    #if UNITY_EDITOR
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ChangeSpanish.Invoke();
        }

        if (Input.GetKey(KeyCode.N))
        {
            ChangeEnglish.Invoke();
        }
    }
    
    #endif
}

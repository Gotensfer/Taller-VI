using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeLanguage : MonoBehaviour
{
    public UnityEvent ChangeSpanish, ChangeEnglish;

    public static int Language; //1 = Español , 0 = Ingles // Dios mio - Jf

    #if UNITY_EDITOR
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.V))
        {
            ChangeSpanish.Invoke();
            Language = 1;
        }

        if (Input.GetKey(KeyCode.N))
        {
            ChangeEnglish.Invoke();
            Language = 0;
        }
    }
    
    #endif

    public void ChangeToSpanish()
    {
        ChangeSpanish.Invoke();
        Language = 1;
    }    

    public void ChangeToEnglish()
    {
        ChangeEnglish.Invoke();
        Language = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIInterface : MonoBehaviour
{
    [SerializeField] int Reset_To;
    public void Reset()
    {
        SceneManager.LoadScene(Reset_To);
    }
    
    
}

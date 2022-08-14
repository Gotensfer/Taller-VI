using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIInterface : MonoBehaviour
{
    [SerializeField] int index;
    public void Reset()
    {
        SceneManager.LoadScene(index);
    }
}

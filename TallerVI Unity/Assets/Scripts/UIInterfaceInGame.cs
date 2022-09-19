using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using System.Net.Sockets;

public class UIInterfaceInGame : MonoBehaviour
{
    public static bool alreadyInitialized = false;
    public void Reset(int sceneIndex)
    {
        if (sceneIndex < 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneIndex);
        alreadyInitialized = true;
    }


}

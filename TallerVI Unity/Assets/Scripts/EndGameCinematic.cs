using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameCinematic : MonoBehaviour
{
    [SerializeField] float timeToChange;

    private void Start()
    {
        Invoke(nameof(ToCongratulateScreen), timeToChange);
    }

    void ToCongratulateScreen()
    {
        SceneManager.LoadScene(4);
    }
}

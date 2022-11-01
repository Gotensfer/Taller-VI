using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumContainerData : MonoBehaviour
{
    [SerializeField] Sprite darkPolaroid;
    [SerializeField] Achievement achievement;

    public void EnablePhoto()
    {
        GetComponent<Image>().sprite = achievement.albumPolaroidImage;
        GetComponent<Button>().interactable = true;
    }

    public void DisablePhoto()
    {
        GetComponent<Image>().sprite = darkPolaroid;
        GetComponent<Button>().interactable = false;
    }

    private void Start()
    {
        // En efecto, que asco de codigo -Juanfer del pasado con el c�digo original

        // En efecto, que c�digo tan hermoso -Juanfer que refactoriz� con el nuevo sistema de logros

        if (PlayerPrefs.GetInt(achievement._name, -1) == 1)
        {
            EnablePhoto();
        }
        else
        {
            DisablePhoto();
        }
    }
}

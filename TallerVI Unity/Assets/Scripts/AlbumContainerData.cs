using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AlbumPhoto
{
    yamcha,
    moonlanding,
    kokun,
    antrunoStark,
    cacalverin
}

public class AlbumContainerData : MonoBehaviour
{
    [SerializeField] AlbumPhoto albumPhoto;
    [SerializeField] Sprite darkPolaroid;
    [SerializeField] Sprite photo;

    public void EnablePhoto()
    {
        GetComponent<Image>().sprite = photo;
        GetComponent<Button>().interactable = true;
    }

    public void DisablePhoto()
    {
        GetComponent<Image>().sprite = darkPolaroid;
        GetComponent<Button>().interactable = false;
    }

    private void Start()
    {
        // En efecto, que asco de codigo

        switch (albumPhoto)
        {
            case AlbumPhoto.yamcha:
                if (PlayerPrefs.GetInt($"Achievement1", -1) == 1) // 2k
                {
                    EnablePhoto();
                }
                else
                {
                    DisablePhoto();
                }
                break;
            case AlbumPhoto.moonlanding:
                if (PlayerPrefs.GetInt($"Achievement5", -1) == 1) // 500m alt
                {
                    EnablePhoto();
                }
                else
                {
                    DisablePhoto();
                }
                break;
            case AlbumPhoto.kokun:
                if (PlayerPrefs.GetInt($"Achievement2", -1) == 1) // 4k
                {
                    EnablePhoto();
                }
                else
                {
                    DisablePhoto();
                }
                break;
            case AlbumPhoto.antrunoStark:
                if (PlayerPrefs.GetInt($"Achievement3", -1) == 1) // 6k
                {
                    EnablePhoto();
                }
                else
                {
                    DisablePhoto();
                }
                break;
            case AlbumPhoto.cacalverin:
                if (PlayerPrefs.GetInt($"Achievement4", -1) == 1) // 8k
                {
                    EnablePhoto();
                }
                else
                {
                    DisablePhoto();
                }
                break;
        }
    }
}

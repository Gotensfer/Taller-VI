using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserTestingUtils : MonoBehaviour
{
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void AddCoins(int amount)
    {
        EconomyData.AddCoins(amount);
    }
}

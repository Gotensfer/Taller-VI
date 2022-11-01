using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpRestraint_TempModule : MonoBehaviour
{
    [SerializeField] GameObject mitosisButton;
    [SerializeField] GameObject fecalitoButton;

    public void DisablePowerUpButtons()
    {
        mitosisButton.SetActive(false);

        if (PlayerPrefs.GetInt($"Fecalito Level") != 0)
        {
            fecalitoButton.SetActive(false);
        }
    }

    public void EnablePowerUpButtons()
    {
        mitosisButton.SetActive(true);
        
        if (PlayerPrefs.GetInt($"Fecalito Level") != 0)
        {
            fecalitoButton.SetActive(true);
        }
    }
}

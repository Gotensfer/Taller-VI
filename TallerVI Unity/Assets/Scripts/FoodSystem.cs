using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // JULIAN HDTPM
using System;
using TMPro;
using UnityEngine.UI;

// Deprecado, no hace nada por ahora, pero la idea es meterlo en un custom editor más adelante (?
public enum FoodID
{
    Soda,
    Beer,
    Guaro,
    Bocachico,
    Ajiaco,
    BandejaPaisa,
    Brevas,
    Flan,
    Bocadillo
}

public class FoodSystem : MonoBehaviour
{
    public List<Food> foods;
    public List<Food> addedFoods;
    public List<GameObject> foodsUnlockables;
    
    FMOD.Studio.EventInstance sfx;

    private void Start()
    {
        for (int i = 0; i < foods.Count; i++)
        {
            if (PlayerPrefs.GetInt(Enum.GetName(typeof(FoodID), i)) == 0)
            {
                foodsUnlockables[i].SetActive(true);
            }
            else if (PlayerPrefs.GetInt(Enum.GetName(typeof(FoodID), i)) == 1)
            {
                foodsUnlockables[i].SetActive(false);
            }
            
        }
    }

    public void BuyFood(int foodID)
    {
        if (EconomyData.coins >= foods[foodID].coinPrice)
        {
            EconomyData.SpendCoins(foods[foodID].coinPrice);

            foodsUnlockables[foodID].SetActive(false);
            //sfx candado
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/Unlock");
            sfx.start();
            PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), foodID)}", 1);
            PlayerPrefs.Save();
        }
        else
        {
            //sfx no money
            sfx = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/UI/No money");
            sfx.start();
        }
    }

    public void AddFood(int foodID) // Cambiar a un enum o con autoresponsabilidad más adelante
    {
        // Eliminar comidas del tipo agregado antes de agregar una comida para evitar tener activo más de una bebida por ejemplo
        for (int i = 0; i < addedFoods.Count; i++)
        {
            if (foods[foodID].type == addedFoods[i].type)
            {
                addedFoods.Remove(addedFoods[i]);
            }
        }

        addedFoods.Add(foods[foodID]);
    }

    public void ApplyAddedFoods()
    {
        foreach (Food food in addedFoods)
        {
            LaunchData.maxVelocity += food.maxVelocity;
            LaunchData.anglePerSecond += food.anglePerSecond;
            LaunchData.impulse += food.impulse;
            LaunchData.bounces += food.bounces;
        }

#if UNITY_EDITOR
        print($"V.m: {LaunchData.maxVelocity}");
        print($"V.a: {LaunchData.anglePerSecond}");
        print($"Impulse: {LaunchData.impulse}");
        print($"Bounces: {LaunchData.bounces}");
#endif
    }

    
    // ESTO NO DEBERIA IR AQUI JUEPUTA JULIAN ARREGLA ESA UI
    public void ChangeToLaunchScene()
    {
        SceneManager.LoadScene(2);
    }

    // ESTO TAMPOCO DEBERIA IR ASI
    // Se realiza para resetear los powerups al volver a la selección de comidas
    private void OnEnable()
    {
        LaunchData.ResetLaunchData();
    }

    private void OnDisable()
    {
        sfx.release();
    }

    // Esto es una muy mala practica - JF
    [SerializeField] TextMeshProUGUI launchTextButton;
    [SerializeField] Image launchImage;
    [SerializeField] Button launchButton;

    Color unavailable = new Color(1, 1, 1, 0.5f);
    Color available = new Color(1, 1, 1, 1);
    private void Update()
    {
        if (addedFoods.Count != 3)
        {
            launchTextButton.alpha = 0.5f;
            launchImage.color = unavailable;
            launchButton.interactable = false;
        }
        else
        {
            launchTextButton.alpha = 1f;
            launchImage.color = available;
            launchButton.interactable = true;
        }
        
    }
}

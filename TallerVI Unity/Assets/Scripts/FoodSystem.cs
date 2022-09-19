using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // JULIAN HDTPM
using System;

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

    private void Start()
    {
        for (int i = 0; i < foods.Count; i++)
        {
            if (PlayerPrefs.GetInt(Enum.GetName(typeof(FoodID), i)) == 0)
            {
                foodsUnlockables[i].SetActive(true);
            }
        }
    }

    public void BuyFood(int foodID)
    {
        if (EconomyData.coins >= foods[foodID].coinPrice)
        {
            EconomyData.SpendCoins(foods[foodID].coinPrice);

            foodsUnlockables[foodID].SetActive(false);

            PlayerPrefs.SetInt($"{Enum.GetName(typeof(FoodID), foodID)}", 1);
            PlayerPrefs.Save();
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
        SceneManager.LoadScene(1);
    }

    // ESTO TAMPOCO DEBERIA IR ASI
    // Se realiza para resetear los powerups al volver a la selección de comidas
    private void OnEnable()
    {
        LaunchData.ResetLaunchData();
    }
}

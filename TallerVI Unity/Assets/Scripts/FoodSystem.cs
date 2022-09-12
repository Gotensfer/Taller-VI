using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddFood(int foodID) // Cambiar a un enum o con autoresponsabilidad más adelante
    {
        // Eliminar comidas del tipo agregado antes de agregar una comida para evitar tener activo más de una bebida por ejemplo
        foreach (Food food in addedFoods)
        {
            if (foods[foodID].type == food.type)
            {
                addedFoods.Remove(food);
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
}

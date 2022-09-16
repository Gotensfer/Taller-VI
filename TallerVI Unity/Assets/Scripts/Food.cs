using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoodType
{
    Drink,
    MainDish,
    Dessert
}

[CreateAssetMenu(fileName = "A food", menuName = "Food", order = 0)]
public class Food : ScriptableObject
{   
    public float maxVelocity;
    public int anglePerSecond;
    public float impulse;
    public int bounces;
    public FoodType type;
}

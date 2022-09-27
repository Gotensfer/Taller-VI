using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISliders : MonoBehaviour
{
    [SerializeField] Slider veloMaxSlider, anguloSlider, impulsoSlider, rebotesSlider;

    [SerializeField] private TMP_Text veloValue, anguloValue, impulsoValue, rebotesValue;

    private FoodSystem fdSys;

    private float velo, angulo, impulso, rebotes;

    private void Awake()
    {
        fdSys = FindObjectOfType<FoodSystem>();
    }

    private void Update()
    {
        Calculate();

        veloValue.text = velo + "km/h";
        anguloValue.text = angulo + "°";
        impulsoValue.text = impulso + "N";
        rebotesValue.text = "+" + rebotes;

        veloMaxSlider.value = velo;
        anguloSlider.value = angulo;
        impulsoSlider.value = impulso;
        rebotesSlider.value = rebotes;
    }

    void Calculate()
    {
        velo = LaunchData.min_MaxVelocity;
        angulo = LaunchData.min_AnglePerSecond;
        impulso = LaunchData.min_Impulse;
        rebotes = LaunchData.min_bounces;
        
        foreach (Food food in fdSys.addedFoods)
        {
            velo += food.maxVelocity;
            angulo += food.anglePerSecond;
            impulso += food.impulse;
            rebotes += food.bounces;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test_FoodSystem : MonoBehaviour
{
    public void AddFood(int strenght)
    {
        Test_LaunchData.strenght += strenght;
    }

    public void GoToLaunchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

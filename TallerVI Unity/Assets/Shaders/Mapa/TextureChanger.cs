using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureChanger : MonoBehaviour
{
    // Start is called before the first frame update
    float secondBGp, thirdBG = 0; //Valor de interpolación entre mapas
    [SerializeField] MeshRenderer textureBG = null;
    float time;

    [SerializeField] bool condicion1, condicion2;
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(condicion1 == true)
        {
            ChangeToSecondMap();

            if(time > 1)
            {
                condicion1= false;
                time = 0;
            }
        }

        if(condicion2 == true)
        {
            ChangeToThirdMap();
            if (time > 1)
            {
                condicion2 = false;
                time = 0;
            }
        }
      

    }

    void ChangeToSecondMap()
    {
        time += Time.deltaTime;
        secondBGp = Mathf.Lerp(0, 1, time);
        textureBG.material.SetFloat("_Condicion1", secondBGp);
    }

    void ChangeToThirdMap()
    {
        time += Time.deltaTime;
        thirdBG = Mathf.Lerp(0, 1, time);
        textureBG.material.SetFloat("_Condicion2", thirdBG);
    }
}

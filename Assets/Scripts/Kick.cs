using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Kick : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider slider;
    float strenght;

    float timesBounce = 0;

    public void Launch()
    {
        player.AddForce(new Vector2(1, 2f) * strenght, ForceMode2D.Impulse);
    }

    private void Update()
    {
        strenght = slider.value * 50;
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timesBounce++;
        if (timesBounce > 1)
        {
            player.drag += 0.2f;
        }
    }
}

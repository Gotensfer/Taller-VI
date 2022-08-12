using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aim_Kick : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider slider;
    [SerializeField] Slider slider2;
    float strenght;
    float angle;
    private Vector2 direction;

    float timesBounce = 0;

    public void Launch()
    {
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
    }

    private void Update()
    {
        strenght = slider.value * 50;
        angle = slider2.value * (90 * Mathf.PI / 180);
        Aim();

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
    private void Aim()
    {
        direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }
}

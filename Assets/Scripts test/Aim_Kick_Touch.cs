using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aim_Kick_Touch : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider slider;
    float strenght;
    private Vector2 direction;

    float timesBounce = 0;
    private bool flag = false;

    public void Launch()
    {
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
        flag = true;
    }

    private void Update()
    {
        strenght = slider.value * 50;
        Aim_Touch();
        Debug.DrawLine(new Vector2(0, 0), direction, Color.blue);

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
    private void Aim_Touch()
    {
        foreach (Touch touch in Input.touches)
        {
            direction = (touch.position - player.position).normalized;
            transform.right = direction;

        }
    }

}

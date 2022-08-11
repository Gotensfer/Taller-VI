using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aim_Kick : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider launchforce;
    float strenght;

    float timesBounce = 0;

    public void Launch()
    {
        player.AddForce(new Vector2(1, 2f) * strenght, ForceMode2D.Impulse);
    }

    private void Update()
    {
        Vector2 playerPosition = player.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        if (Input.GetMouseButton(0))
        {
            Launch();
            
        }
        strenght = launchforce.value * 50;
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

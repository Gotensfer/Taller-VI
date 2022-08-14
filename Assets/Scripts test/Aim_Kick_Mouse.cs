using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aim_Kick_Mouse : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider slider;
    float strenght;
    private Vector2 direction;

    

    float timesBounce = 0;

    public void Launch()
    {
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
    }
    private bool flag = false;
    private void Update()
    {
        strenght = slider.value * 50;
        Aim_Mouse();
        Debug.DrawLine(new Vector3(0, 0, 0), direction, Color.blue);


        if (Input.GetMouseButtonDown(1) && !flag)
        {
            Launch();
            flag = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timesBounce++;
        if (timesBounce > 1)
        {
            player.drag += 0.2f;
        }
    }
    private void Aim_Mouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (mousePosition - player.position).normalized;
        transform.right = direction;
    }

}

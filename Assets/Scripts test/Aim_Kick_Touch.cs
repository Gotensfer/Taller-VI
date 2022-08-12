using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Aim_Kick_Touch : MonoBehaviour
{
    [SerializeField] Rigidbody2D player;
    [SerializeField] Slider slider;
    [SerializeField] RectTransform m_xButtonRect;
    [SerializeField] RectTransform m_xSliderRect;
    [SerializeField] Canvas m_xMenuManager;
    [SerializeField] GameObject Text;

    float strenght;
    private Vector2 direction;

    float timesBounce = 0;
    private bool flag = false;

    public void Launch()
    {
        Text.SetActive(false);
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
        flag = true;
    }

    private void Update()
    {
        strenght = slider.value * 50;
        Aim_Touch();
        Debug.DrawLine(new Vector3(0, 0, 0), direction, Color.blue);
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

            if (!ButtonContainsPosition(touch.position))
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                direction = (touchPosition - player.position).normalized;
                transform.right = direction;
            }

        }
    }
    public bool ButtonContainsPosition(Vector2 xPos)
    {

        float fMinX = m_xButtonRect.transform.position.x - ((m_xButtonRect.sizeDelta.x * 0.5f) * m_xMenuManager.scaleFactor);
        float fMaxX = m_xButtonRect.transform.position.x + ((m_xButtonRect.sizeDelta.x * 0.5f) * m_xMenuManager.scaleFactor);
        float fMinY = m_xButtonRect.transform.position.y - ((m_xButtonRect.sizeDelta.y * 0.5f) * m_xMenuManager.scaleFactor);
        float fMaxY = m_xButtonRect.transform.position.y + ((m_xButtonRect.sizeDelta.y * 0.5f) * m_xMenuManager.scaleFactor);

        float sMinX = m_xSliderRect.transform.position.x - ((m_xSliderRect.sizeDelta.x * 0.5f) * m_xMenuManager.scaleFactor);
        float sMaxX = m_xSliderRect.transform.position.x + ((m_xSliderRect.sizeDelta.x * 0.5f) * m_xMenuManager.scaleFactor);
        float sMinY = m_xSliderRect.transform.position.y - ((m_xSliderRect.sizeDelta.y * 0.5f) * m_xMenuManager.scaleFactor);
        float sMaxY = m_xSliderRect.transform.position.y + ((m_xSliderRect.sizeDelta.y * 0.5f) * m_xMenuManager.scaleFactor);


        if (xPos.x <= fMaxX && xPos.x >= fMinX)
        {
            if (xPos.y <= fMaxY && xPos.y >= fMinY)
            {
                return true;
            }
        }

        if (xPos.x <= sMaxX && xPos.x >= sMinX)
        {
            if (xPos.y <= sMaxY && xPos.y >= sMinY)
            {
                return true;
            }
        }

        return false;
    }
}

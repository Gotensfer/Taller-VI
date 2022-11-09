using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fecalito : MonoBehaviour
{
    [SerializeField] float strenght;
    [SerializeField] Vector2 direction;

    public int level;

    [SerializeField] float cdTimeLvl1, cdTimeLvl2, cdTimeLvl3;

    private float cdTime;

    public float CdTime
    {
        get => cdTime;
    }
    
    public float remainingCD { get; private set; }

    [SerializeField] Rigidbody2D player;

    [SerializeField] GameObject button;

    [SerializeField] PlayerEvents_Interface playerEvents;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Fecalito Level", -1);

        button.SetActive(false);
        playerEvents.LaunchEvent.AddListener(EnableFecalitoButton); // Para evitar ruido en el EventManager

        switch (level)
        {
            // Cambiar esto de hardcoded a un GameData manager o similares
            case 0:
                playerEvents.LaunchEvent.RemoveListener(EnableFecalitoButton); // No activar fecalito si no se posee
                break;
            case 1:
                cdTime = cdTimeLvl1;
                break;
            case 2:
                cdTime = cdTimeLvl2;
                break;
            case 3:
                cdTime = cdTimeLvl3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE MITOSIS");
                break;
        }
    }

    private void Update()
    {
        if (remainingCD > 0)
        {
            remainingCD -= Time.deltaTime;
            if (remainingCD <= 0)
            {
                button.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void Activate_Fecalito()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0); // M�s que todo para lograr un mejor efecto visual
        player.AddForce(direction * strenght, ForceMode2D.Impulse);
        
        remainingCD = cdTime;
        button.GetComponent<Button>().interactable = false;

        playerEvents.FecalitoEvent.Invoke();
    }

    void EnableFecalitoButton()
    {
        button.SetActive(true);
    }
}

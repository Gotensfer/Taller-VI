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

    [SerializeField] float cdTime;
    float remainingCD;

    [SerializeField] int maxCharges;
    [SerializeField] int charges;

    [SerializeField] Rigidbody2D player;

    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI chargeIndicator;

    [SerializeField] Sprite availableSprite;
    [SerializeField] Sprite onCDSprite;

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
                maxCharges = 0;
                break;
            case 1:
                maxCharges = 1;
                break;
            case 2:
                maxCharges = 2;
                break;
            case 3:
                maxCharges = 3;
                break;
            default:
                Debug.LogError("NO SE CARGARON LOS DATOS DE NIVEL DE MITOSIS");
                break;
        }

        charges = maxCharges;
        UpdateUIChargeIndicator();
    }

    private void Update()
    {
        if (remainingCD > 0 && charges > 0)
        {
            remainingCD -= Time.deltaTime;
            if (remainingCD <= 0)
            {
                button.GetComponent<Button>().interactable = true;
                button.GetComponent<Image>().sprite = availableSprite;
            }
        }
    }

    public void Activate_Fecalito()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0); // Más que todo para lograr un mejor efecto visual
        player.AddForce(direction * strenght, ForceMode2D.Impulse);

        charges--;
        remainingCD = cdTime;
        button.GetComponent<Button>().interactable = false;
        button.GetComponent<Image>().sprite = onCDSprite;
        UpdateUIChargeIndicator();

        playerEvents.FecalitoEvent.Invoke();
    }

    void EnableFecalitoButton()
    {
        button.SetActive(true);
    }

    public void AddCharge()
    {
        charges = Mathf.Clamp(charges + 1, 0, maxCharges);
        UpdateUIChargeIndicator();
    }

    void UpdateUIChargeIndicator()
    {
        chargeIndicator.text = $"x{charges}";
    }
}

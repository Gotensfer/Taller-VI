using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mitosis : MonoBehaviour
{
    [SerializeField] float strenght;
    [SerializeField] Vector2 direction;

    [SerializeField] float cdTime;
    float remainingCD;

    public int level;

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
        button.SetActive(false);
        playerEvents.LaunchEvent.AddListener(EnableMitosisButton); // Para evitar ruido en el EventManager

        switch (level)
        {
            // Cambiar esto de hardcoded a un GameData manager o similares
            case 1:
                maxCharges = 1;
                break;
            case 2:
                maxCharges = 2;
                break;
            case 3:
                maxCharges = 3;
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

    public void Activate_Mitosis()
    {
        direction.Normalize();
        player.velocity = new Vector2(player.velocity.x, 0);
        player.AddForce(direction * strenght, ForceMode2D.Impulse);

        charges--;
        remainingCD = cdTime;
        button.GetComponent<Button>().interactable = false;
        button.GetComponent<Image>().sprite = onCDSprite;
        UpdateUIChargeIndicator();

        playerEvents.MitosisEvent.Invoke();
    }

    void EnableMitosisButton()
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

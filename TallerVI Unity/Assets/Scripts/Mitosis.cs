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

    public float CdTime
    {
        get => cdTime;
    }
    
    public float remainingCD { get; private set; }

    public int level;

    [SerializeField] int maxCharges;
    [SerializeField] int charges;

    [SerializeField] Rigidbody2D player;

    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI chargeIndicator;

    [SerializeField] PlayerEvents_Interface playerEvents;

    private void Start()
    {
        level = PlayerPrefs.GetInt("Mitosis Level", -1);

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

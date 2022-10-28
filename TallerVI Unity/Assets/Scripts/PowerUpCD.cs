using UnityEngine;
using Image = UnityEngine.UI.Image;

public class PowerUpCD : MonoBehaviour
{
    [SerializeField] private Image mitosisButton, fecalitoButton;
    [SerializeField] private Mitosis mitosis;
    [SerializeField] private Fecalito fecalito;

    private bool pressedF, pressedM;

    private void Update()
    {
        if (pressedM)
        {
            mitosisButton.fillAmount = Mathf.Abs(mitosis.remainingCD-mitosis.CdTime)/mitosis.CdTime;
        }

        if (pressedF)
        {
            fecalitoButton.fillAmount = Mathf.Abs(fecalito.remainingCD-fecalito.CdTime)/fecalito.CdTime;
        }
    }

    public void PressedF()
    {
        pressedF = true;
    }

    public void PressedM()
    {
        pressedM = true;
    }
}

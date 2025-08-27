using UnityEngine;
using TMPro;

public class ComboUI : MonoBehaviour
{
    public TextMeshProUGUI comboText;

    public void AddCombo(int comboCount)
    {
        comboText.text = $"Combo x{comboCount}!";
    }

    public void ResetCombo()
    {
        comboText.text = "";
    }
}


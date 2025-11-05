using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public int totalItems = 0;
    public int collectedItems = 0;

    [Header("UI")]
    public TMP_Text itemCountText;

    void Start()
    {
        collectedItems = 0;
        UpdateUI();

        // Garante que o jogo esteja rodando
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (itemCountText != null)
            itemCountText.text = "Itens: " + collectedItems + " / " + totalItems;
    }
}

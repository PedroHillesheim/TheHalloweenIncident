using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public TMP_Text itemCounterText;
    public GameObject victoryPanel;

    private int totalItems = 0;
    private int collectedItems = 0;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);

        UpdateUI();
    }

    public void RegisterItem()
    {
        totalItems++;
        UpdateUI();
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();

        if (collectedItems >= totalItems && totalItems > 0)
        {
            Debug.Log("Vitória!");
            if (victoryPanel != null)
                victoryPanel.SetActive(true);
        }
    }

    void UpdateUI()
    {
        if (itemCounterText != null)
            itemCounterText.text = $"Itens: {collectedItems}/{totalItems}";
    }
}

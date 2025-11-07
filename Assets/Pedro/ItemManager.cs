using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public int totalItems = 0;
    public int collectedItems = 0;

    [Header("Referências")]
    public UnityEvent victoryPanel;
    public TMP_Text itemCountText;

    void Start()
    {
        collectedItems = 0;
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();

        if (collectedItems >= totalItems)
            WinGame();
    }

    void UpdateUI()
    {
        if (itemCountText != null)
            itemCountText.text = "Itens: " + collectedItems + " / " + totalItems;
    }

    void WinGame()
    {
        victoryPanel.Invoke();
        Time.timeScale = 0f;
    }
}

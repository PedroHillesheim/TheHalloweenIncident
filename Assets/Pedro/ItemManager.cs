using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public TMP_Text itemCounterText;    // Texto que mostra quantos itens foram coletados
    public GameObject victoryScreen;    // Tela de vitória

    private int totalItems;
    private int collectedItems;

    void Start()
    {
        // Conta quantos itens existem na cena (todos com o script ItemPickup)
        totalItems = FindObjectsOfType<ItemPick>().Length;
        collectedItems = 0;

        UpdateUI();

        if (victoryScreen != null)
            victoryScreen.SetActive(false); // Esconde a tela no início
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();

        // Verifica se pegou tudo
        if (collectedItems >= totalItems)
        {
            ShowVictoryScreen();
        }
    }

    void UpdateUI()
    {
        if (itemCounterText != null)
            itemCounterText.text = $"Itens: {collectedItems}/{totalItems}";
    }

    void ShowVictoryScreen()
    {
        if (victoryScreen != null)
            victoryScreen.SetActive(true);
    }
}

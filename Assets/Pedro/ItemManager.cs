using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Header("Contagem")]
    public int collectedItems = 0;
    public int totalItems = 4; // 🔥 TOTAL FIXO

    private List<string> collectedIDs = new List<string>();
    private List<TMP_Text> registeredTexts = new List<TMP_Text>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterUIText(TMP_Text txt)
    {
        if (!registeredTexts.Contains(txt))
            registeredTexts.Add(txt);

        UpdateUI();
    }

    // NÃO REGISTRA MAIS ITEMS — TOTAL É FIXO
    public void RegisterItem(string id)
    {
        // deixa vazio propositalmente
    }

    public void CollectItem(string id)
    {
        if (!collectedIDs.Contains(id))
        {
            collectedIDs.Add(id);
            collectedItems++;
            UpdateUI();
        }
    }

    public bool HasCollected(string id)
    {
        return collectedIDs.Contains(id);
    }

    public void ResetProgress()
    {
        collectedItems = 0;
        collectedIDs.Clear();
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var txt in registeredTexts)
        {
            txt.text = collectedItems + " / " + totalItems;
        }
    }
}

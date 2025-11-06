using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    private TMP_Text textUI;

    void Start()
    {
        textUI = GetComponent<TMP_Text>();

        // Tenta conectar com o ItemManager global
        if (ItemManager.Instance != null)
        {
            // Atualiza a referência do texto
            ItemManager.Instance.itemCountText = textUI;
            ItemManager.Instance.UpdateUI();
        }
        else
        {
            Debug.LogWarning("ItemManager não encontrado nesta cena!");
        }
    }
}

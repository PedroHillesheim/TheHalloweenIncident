using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public string itemID; // cada item deve ter um ID único

    void Start()
    {
        // Se já coletado antes, esconde o item
        if (ItemManager.Instance.HasCollected(itemID))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!ItemManager.Instance.HasCollected(itemID))
            {
                ItemManager.Instance.CollectItem(itemID);
                gameObject.SetActive(false);
            }
        }
    }
}

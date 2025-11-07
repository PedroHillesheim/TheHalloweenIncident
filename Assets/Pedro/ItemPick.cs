using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public string itemID; // ID único por item
    public float pickupRange = 1.5f;

    private Transform player;
    private ItemManager manager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        manager = ItemManager.Instance;

        // Se o item já foi coletado, ele desaparece
        if (manager != null && manager.HasCollected(itemID))
        {
            Destroy(gameObject);
            return;
        }

        // Registra o item no total apenas na primeira vez
        if (manager != null)
            manager.RegisterItem(itemID);
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= pickupRange)
            CollectItem();
    }

    void CollectItem()
    {
        if (manager != null)
            manager.CollectItem(itemID);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

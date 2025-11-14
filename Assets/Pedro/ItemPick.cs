using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public string itemID;
    public float pickupRange = 1.5f;

    private Transform player;
    private ItemManager manager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        manager = ItemManager.Instance;

        if (manager != null)
        {
            // Se já foi coletado antes, some
            if (manager.HasCollected(itemID))
            {
                Destroy(gameObject);
                return;
            }

            // Conta item no total
            manager.RegisterItem(itemID);
        }
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist <= pickupRange)
            Collect();
    }

    void Collect()
    {
        if (manager != null)
            manager.CollectItem(itemID);

        Destroy(gameObject);
    }
}

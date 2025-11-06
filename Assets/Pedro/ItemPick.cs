using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public float pickupRange = 1.5f;

    private Transform player;
    private ItemManager manager;
    private PersistentItem persistentData;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        manager = ItemManager.Instance;
        persistentData = GetComponent<PersistentItem>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= pickupRange)
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (manager != null)
            manager.AddItem();

        if (persistentData != null)
            persistentData.MarkAsCollected(); // Salva o item como coletado

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

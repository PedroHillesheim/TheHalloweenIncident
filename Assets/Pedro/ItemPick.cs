using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public float pickupRange = 1.5f;

    private Transform player;
    private ItemManager manager;

    void Start()
    {
        // Acha o jogador pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // Acha o gerenciador
        manager = FindFirstObjectByType(typeof(ItemManager)) as ItemManager;
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
        // Notifica o gerenciador
        if (manager != null)
            manager.AddItem();

        // Destroi o item
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public float pickupRange = 1.5f; // distância de coleta

    private Transform player;
    private ItemManager manager;

    void Start()
    {
        // Acha o jogador pela tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        // Acha o gerenciador de itens na cena
        manager = FindObjectOfType<ItemManager>();
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

        // Destrói o item
        Destroy(gameObject);
    }

    // Gizmo pra ver o alcance no editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}

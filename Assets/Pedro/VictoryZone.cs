using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    public string nextSceneName; // nome da próxima cena
    public bool requireAllItems = true; // precisa pegar todos os itens?

    private ItemManager itemManager;
    private bool canTeleport = false;

    void Start()
    {
        itemManager = ItemManager.Instance;

        // Garante que o collider está como Trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null && !col.isTrigger)
            col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Garante que temos um ItemManager
        if (itemManager == null)
            itemManager = ItemManager.Instance;

        // Checa se o jogador pode vencer
        if (!requireAllItems || itemManager.collectedItems >= itemManager.totalItems)
        {
            canTeleport = true;
            Debug.Log("✅ Todos os itens coletados! Entrando na zona de vitória...");
            LoadNextScene();
        }
        else
        {
            Debug.Log("⚠️ Ainda há itens para coletar!");
        }
    }

    void LoadNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("⚠️ Nenhuma cena configurada em VictoryZone!");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }
}

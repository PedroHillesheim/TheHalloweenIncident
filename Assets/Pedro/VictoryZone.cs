using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    public string nextSceneName;

    private ItemManager itemManager;

    void Start()
    {
        itemManager = FindFirstObjectByType<ItemManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemManager != null && itemManager.collectedItems >= itemManager.totalItems)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("Ainda há itens para coletar!");
            }
        }
    }
}

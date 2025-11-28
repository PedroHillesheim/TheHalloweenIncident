using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPick : MonoBehaviour
{
    public static int collectedItems = 0;
    public int totalItems = 4;
    public GameObject porta;
    public TMP_Text collectedItemstotal;
    private void Start()
    {
        collectedItemstotal.text = collectedItems + "/" + totalItems;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collectedItems++;
            gameObject.SetActive(false);
            collectedItemstotal.text = collectedItems + "/" + totalItems;
            if (collectedItems == totalItems && porta != null)
            {
                Destroy(porta);
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        collectedItems = 0;
    }
}

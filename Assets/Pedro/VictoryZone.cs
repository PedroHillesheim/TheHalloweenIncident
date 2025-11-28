using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryZone : MonoBehaviour
{
    public int totalItemsRequired = 4;
    public string sceneName = " ";
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && ItemPick.collectedItems == totalItemsRequired)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;

        // 🔥 RESET TOTAL
        ItemManager.Instance.ResetProgress();

        // 🔥 RECARREGA para trazer os itens de volta
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

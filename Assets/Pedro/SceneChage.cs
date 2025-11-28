using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChage : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}

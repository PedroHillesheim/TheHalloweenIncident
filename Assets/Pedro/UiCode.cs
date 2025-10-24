using UnityEngine;
using UnityEngine.SceneManagement;

public class UiCode : MonoBehaviour
{
    public void SceneChangeR(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}

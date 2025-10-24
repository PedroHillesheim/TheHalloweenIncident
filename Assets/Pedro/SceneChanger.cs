using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void SceneChangeR(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

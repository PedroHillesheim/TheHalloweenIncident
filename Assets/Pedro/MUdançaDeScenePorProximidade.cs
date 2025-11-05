using UnityEngine;
using UnityEngine.SceneManagement;

public class MUdançaDeScenePorProximidade : MonoBehaviour
{
    public string sceneName = "NomeDaNovaCena"; // Nome da cena para carregar
    public float triggerDistance = 3f; // Dist�ncia para trocar de cena
    public Transform player; // Refer�ncia ao player

    private bool isChanging = false;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= triggerDistance && !isChanging)
        {
            isChanging = true;
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Header("Configurações")]
    public int totalItems = 0;
    public int collectedItems = 0;

    [Header("Referências")]
    public UnityEvent victoryPanel;
    public TMP_Text itemCountText;

    void Awake()
    {
        // Impede que seja destruído ao trocar de cena
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Tenta achar o novo texto (caso exista)
        TMP_Text foundText = FindFirstObjectByType<TMP_Text>();
        if (foundText != null)
            itemCountText = foundText;

        UpdateUI();
    }

    public void AddItem()
    {
        collectedItems++;
        UpdateUI();

        if (collectedItems >= totalItems)
        {
            WinGame();
        }
    }

    public void UpdateUI()
    {
        if (itemCountText != null)
            itemCountText.text = "Itens: " + collectedItems + " / " + totalItems;
    }

    void WinGame()
    {
        if (victoryPanel != null)
        {
            victoryPanel.Invoke();
        }
        else
        {
            Debug.Log("Todos os itens coletados! (Ação de vitória aqui)");
        }
    }
}

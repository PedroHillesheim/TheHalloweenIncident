using UnityEngine;

public class PersistentItem : MonoBehaviour
{
    private string uniqueID;

    void Start()
    {
        // Cria um ID único com base no nome e posição do item
        uniqueID = gameObject.name + "_" + transform.position.ToString();

        // Se esse item já foi coletado, destrói automaticamente
        if (PlayerPrefs.HasKey(uniqueID))
            Destroy(gameObject);
    }

    public void MarkAsCollected()
    {
        PlayerPrefs.SetInt(uniqueID, 1);
        PlayerPrefs.Save();
    }
}

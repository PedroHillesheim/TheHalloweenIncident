using TMPro;
using System.Collections;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    TMP_Text txt;

    void Awake() => txt = GetComponent<TMP_Text>();

    void Start()
    {
        TryRegister();
    }

    void TryRegister()
    {
        if (ItemManager.Instance != null)
            ItemManager.Instance.RegisterUIText(txt);
        else
            StartCoroutine(TryRegisterNextFrame());
    }

    IEnumerator TryRegisterNextFrame()
    {
        yield return null;
        if (ItemManager.Instance != null)
            ItemManager.Instance.RegisterUIText(txt);
    }
}

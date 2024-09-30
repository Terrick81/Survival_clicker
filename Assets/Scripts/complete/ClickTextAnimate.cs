using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickTextAnimate : MonoBehaviour
{
    public void UpdateText(string text)
    {
        GetComponent<TextMeshProUGUI>().text = text;
    }
    private void Start()
    {
        Invoke("DestroyThis", 1);
    }
    
    private void DestroyThis()
    {
        Destroy(gameObject);
    }
}

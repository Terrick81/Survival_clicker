using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScaleBoxCollader : MonoBehaviour
{
    void Start()
    {
        Invoke("G", 1);
    }

    private void G()
    {
        BoxCollider2D _box = GetComponent<BoxCollider2D>();
        RectTransform _rectTransform = GetComponent<RectTransform>();
        _box.size = new Vector2(_rectTransform.rect.width, _rectTransform.rect.height);
    }
}

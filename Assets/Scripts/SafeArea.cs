using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    RectTransform _RT_;
    [SerializeField] int offset = 0;
    private void Awake()
    {
        _RT_ = GetComponent<RectTransform>();
        _RT_.localPosition = new Vector3(Screen.safeArea.x + offset, _RT_.localPosition.y, _RT_.localPosition.z);
    }
}

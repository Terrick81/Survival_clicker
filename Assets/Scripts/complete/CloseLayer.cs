using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloseLayer : MonoBehaviour
{
    [SerializeField] GameObject _shop;
    
    private void OnMouseDown()
    {
        _shop.SetActive(false);
    }
}

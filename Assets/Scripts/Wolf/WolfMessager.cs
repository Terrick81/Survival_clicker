using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMessager : MonoBehaviour
{

    [SerializeField] WolfManager wolfManager;

    private void OnDisable()
    {
        wolfManager.SwithPBW();
        Destroy(this);
    }
}

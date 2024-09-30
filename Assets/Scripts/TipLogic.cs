using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;

public class TipLogic : MonoBehaviour
{
    GameObject _tip_body;
    private LocalizeStringEvent _tip_text;
    static int _id_tip = 0;

    private void Start()
    {
        _tip_body = GameObject.Find("tipBody");
        _tip_body.SetActive(false);
        _tip_text = _tip_body.transform.GetChild(3).GetComponent<LocalizeStringEvent>();
    }
    public void PlayTip(int id_tip, int delay = 0)
    {
        _id_tip = id_tip;
        if (delay > 0)
            Invoke("PlayTipBody", delay);
        else 
            PlayTipBody();
    }

    private void PlayTipBody()
    {
        if (_tip_body.activeInHierarchy)
        {
            _tip_body.SetActive(false);
        }
        _tip_body.SetActive(true);
        _tip_text.StringReference.SetReference("UI", "Tip_" + _id_tip);
        Invoke("CloseTip", 22);
    }
    private void CloseTip()
    {
        _tip_body.SetActive(false);
    }

}

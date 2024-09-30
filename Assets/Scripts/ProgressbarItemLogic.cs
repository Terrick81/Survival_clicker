using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarItemLogic : MonoBehaviour
{
    private Slider _slider_script;
    private TextMeshProUGUI _text;
    private float _targer_progress = 0f;
    private float _fill_speed;
    private bool _change_target = false;

    private void Awake()
    {
        _slider_script = GetComponent<Slider>();
        _text = gameObject.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }


    public void UpdateProgressbar(byte lvl, GameObject new_obj, bool lvlUp = false)
    {
        if (lvlUp)
        {
            _slider_script.value = (lvl-1) / (float)Constants.max_lvl;
            _fill_speed = 0.005f;
        }
        else
        {
            _slider_script.value = 0f;
            _fill_speed = 0.01f;
        }
        
        _change_target = true;
        _targer_progress = lvl / (float)Constants.max_lvl;

        if(lvl == Constants.max_lvl && new_obj == null)
        {
            _text.text = "MAX";  
        }
        else
        {
            _text.text = "X" + lvl;
        }
    }

    public void Update()
    {
        if (_change_target)
        {
            if (_slider_script.value < _targer_progress)
            {
                _slider_script.value += _fill_speed;
            }
            else
            {
                _change_target = false;
            }
        }
    }
}

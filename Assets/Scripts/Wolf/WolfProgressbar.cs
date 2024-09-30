using UnityEngine;
using UnityEngine.UI;


public class WolfProgressbar : MonoBehaviour
{

    private Slider _slider_script;
    private float _targer_progress = 0f;
    private readonly float _fill_speed = 0.008f;
    private bool _change_target = false;

    private void Awake()
    {
        _slider_script = GetComponent<Slider>();
    }

    public void OnEnable()
    {
        _slider_script.value = 1f;
        _change_target = false;
    }
    public void UpdateProgressbar(int count, int max_count)
    {
        _change_target = true;
        _targer_progress = (float)count / (float)max_count;
        if (_slider_script.value < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        if (_change_target)
        {
            if (_slider_script.value > _targer_progress)
            {
                _slider_script.value -= _fill_speed;
            }
            else
            {
                _change_target = false;
            }
            if (_targer_progress == 0)
            {
                _slider_script.value = 0;
            }
        }
    }
}

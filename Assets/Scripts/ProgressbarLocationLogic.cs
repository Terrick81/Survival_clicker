using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressbarLocationLogic : MonoBehaviour
{
    private GameObject[] _steps;  
    private Slider _slider;
    private ParticleSystem _particle_system;

    private float _targer_progress = 0f;
    private float _fill_speed = 0.0006f;
    private bool _change_target = false;
    private Color blue_color = new Color(0f, 0.18f, 0.38f, 0.94f);
    private void Awake()
    {
        _particle_system = GameObject.Find("particle_1").GetComponent<ParticleSystem>();
        _slider = GetComponent<Slider>();
        GameObject steps = transform.GetChild(2).gameObject;
        _steps = new GameObject[steps.transform.childCount];
        for (int i = 0; i < steps.transform.childCount; i++) 
        {
            _steps[i] = steps.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject;
        }
        
    }

    public void UpdateProgressbar(int buy, int need)
    {
        _targer_progress = ((float)buy / need) * 0.2f + ((float)(GameManager.current_location-1) / 5f);
        if (_targer_progress > _slider.value)
        {
            _change_target = true;
            _particle_system.Play();
        } 
    }

    public void UpdateLocation()
    {
        _particle_system.Stop();
        int g = GameManager.current_location - 1;
        _slider.value = g / 5f;
        for (int i = 1; i < (g + 1); i++)
        {
            _steps[i].GetComponent<Image>().color = Color.white;
            _steps[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = blue_color;
        }
    }


    public void Update()
    {
        if (_change_target)
        {
            if(_slider.value < _targer_progress)
            {
                _slider.value += _fill_speed;
            }
            else
            {
                _particle_system.Stop();
                _change_target = false;
            }
        }
    }

}

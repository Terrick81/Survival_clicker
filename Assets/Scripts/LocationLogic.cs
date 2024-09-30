using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class LocationLogic : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites_background;
    [SerializeField] private GameObject[] _location;
    [SerializeField] private GameObject _name_location;
    [SerializeField] private GameObject _walk_layer;
    [SerializeField] private SmokeFireController[] _smokes;
    [SerializeField] private TextMeshProUGUI _time_text;

    private Image _image_background;
    private Image _image_background_helper;

    private void Awake()
    {
        _image_background = GameObject.Find("background").GetComponent<Image>();
        _image_background_helper = GameObject.Find("background_Helper").GetComponent<Image>();
    }

    public void PlayWalk()
    {
        _walk_layer.GetComponent<Animation>().Play();
        _walk_layer.GetComponent<AudioSource>().Play();
    }

    public void ChangeLocation(int current_location)
    {
        if (0 < current_location && current_location <= _sprites_background.Length)
        {
            Invoke("NameAnable", 1.5f);
            foreach (var smoke in  _smokes)
            {
                if(smoke != null)
                    smoke.SwithLocationSmoke(current_location);
            }

            _image_background.sprite = _sprites_background[current_location - 1];
            _image_background_helper.sprite = _sprites_background[current_location - 1];
            if (current_location <= _location.Length)
            {
                if (current_location != 1)
                {
                    _location[current_location - 2].SetActive(false);
                }
                _location[current_location - 1].SetActive(true);
            } 
        }
        if(current_location == 6)
        {
            TimeSpan time = TimeSpan.FromSeconds(YandexGame.savesData.timeToComplete);
            _time_text.text = time.ToString("hh':'mm':'ss");
            if (YandexGame.savesData.better_time > YandexGame.savesData.timeToComplete)
            {
                Debug.Log(YandexGame.savesData.timeToComplete);
                YandexGame.NewLBScoreTimeConvert(Constants.yandex_LB_name, YandexGame.savesData.timeToComplete);
                YandexGame.savesData.better_time = YandexGame.savesData.timeToComplete;
                SaveData.SaveDataFunc();
            }
        }
    }


    private void NameAnable() 
    {
        _name_location.SetActive(true);
        _name_location.GetComponent<Animation>().Play();
        _name_location.GetComponent<AudioSource>().Play();
        LocalizeStringEvent name_text = _name_location.transform.GetChild(1).GetComponent<LocalizeStringEvent>();
        name_text.StringReference.SetReference("UI", "label_location_" + GameManager.current_location);
        Invoke("NameDisable", 7);
    }
    private void NameDisable()
    {
        _name_location.SetActive(false);
    }
}

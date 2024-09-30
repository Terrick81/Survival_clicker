using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using static UnityEngine.AudioSettings;
using YG;
using Unity.VisualScripting;

public class TextInstantiate : MonoBehaviour
{
    int offset = Screen.height/50;
    
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _parent;
    private static GameObject _obj;
    private string _text;

    public void CreateText(int number, Vector3 coord)
    {
        Instantiatetext(coord);

        if (number > 0)
        {
            _text = "+" + Constants.ConvertNumber(number);
        }
        else
        {

            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("en"))
            {
                _text = "There is no tool";
            }
            else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("ru"))
            {
                _text = "нет инструмента";
            }
            else if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("tr"))
            {
                _text = "dogru araç yok";
            }
        }
        _obj.GetComponent<ClickTextAnimate>().UpdateText(_text);

    }

    private void Instantiatetext(Vector3 position)
    {
        _obj = Instantiate(_prefab, _parent.transform);
        position = Camera.main.ScreenToWorldPoint(position);
        position.z = 200;
        _obj.transform.position = position;
        position = _obj.GetComponent<RectTransform>().localPosition;
        position.x += Random.Range(-offset, offset);
        position.y += Random.Range(-offset, offset);
        position.z = 0;
        _obj.GetComponent<RectTransform>().localPosition = position;

    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using YG;
using static Unity.Collections.AllocatorManager;

public class AdScript : MonoBehaviour
{
    private sbyte _timer;

    [SerializeField] private LocalizedString _localizedString;
    [SerializeField] private TextMeshProUGUI _ad_text;
    [SerializeField] private GameObject _table;

    private void Awake()
    {
        _localizedString.Arguments = new object[] { _timer };
    }
    public void OnEnable()
    {
        _timer = Constants.ad_timer;
        _localizedString.StringChanged += UpdateText;
        Invoke("OnBlock", 2.5f);
        StartCoroutine(TimerAd());
    }
    public void OnDisable()
    {
        _localizedString.StringChanged -= UpdateText;
    }

    private void UpdateText(string value)
    {
        _ad_text.text = value;
        
    }

    private void OnBlock()
    {
        _table.GetComponent<Collider2D>().enabled = true;
    }

    IEnumerator TimerAd()
    {
        while (_timer > 0)
        {
            _timer--;
            _localizedString.Arguments[0] = _timer;
            _localizedString.RefreshString();
            if (_timer == 0)
            {
                _table.GetComponent<Collider2D>().enabled = false;
                _table.SetActive(false);
                SaveData.SaveDataFunc();
                YandexGame.FullscreenShow();
            }
            yield return new WaitForSeconds(1);
        }
    }
}

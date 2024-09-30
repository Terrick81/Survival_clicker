using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] Image _image;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] AudioSource _audioSource;
    sbyte timer;

    private void OnEnable()
    {
        _audioSource.PlayDelayed(3);
        timer = 5;
        TimerGift();
    }
        private void TimerGift()
    {
        timer--;
        if (timer < 0) 
        {
            gameObject.SetActive(false);
            return;
        }
        Invoke("TimerGift", 1);
    }

    public void SetText(int type, int profit)
    {
        _image.sprite = _sprites[type];
        _textMeshProUGUI.text = "+" + Constants.ConvertNumber(profit);
    }
}

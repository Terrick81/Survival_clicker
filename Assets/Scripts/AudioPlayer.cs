using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    static GameObject _shop_layer;
    static GameObject _current;
    [SerializeField] private AudioClip _audioClipDown;
    [SerializeField] private AudioClip _audioClipEnter;
    void Start()
    {
        if(_shop_layer == null)
        {
            _shop_layer = GameObject.Find("shop_layer");
        }
        if(_current == null)
        {
            _current = gameObject;
        }
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void OnMouseDown()
    {
        if (!_shop_layer.activeInHierarchy || _current != gameObject)
        {
            _audioSource.PlayOneShot(_audioClipDown);
            _current = gameObject;
        }
    }
    private void OnMouseEnter()
    {
        _audioSource.PlayOneShot(_audioClipEnter, Random.Range(0.7f, 1f));
    }
}

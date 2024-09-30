using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static YG.YandexGame;

public class ScaleUI : MonoBehaviour
{
    [SerializeField] private RectTransform _RectTransform_children;
    [SerializeField] private RectTransform _RectTransform_parent;

    Vector2 old_screen_size;
    void Start()
    {
        _RectTransform_children = GetComponent<RectTransform>();
        old_screen_size = _RectTransform_parent.sizeDelta;
        _RectTransform_children.sizeDelta = _RectTransform_parent.sizeDelta;
        //if(!Instance.infoYG.playerInfoSimulation.isMobile)
        InvokeRepeating("Handler", 0, 0.5f);
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }



    public void Handler()
    {
        if (old_screen_size != _RectTransform_parent.sizeDelta)
        {
            if (Screen.orientation !=  ScreenOrientation.Portrait)
            {
                _RectTransform_children.sizeDelta = _RectTransform_parent.sizeDelta;
            }
                
        }
    }
}

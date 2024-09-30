using System.Collections;
using UnityEngine;
using YG;

public class Wolf : MonoBehaviour
{
    private GameObject _present;
    private GameObject _ad;
    private Animation _click_animate;
    
    static private WolfManager _wolf_manager;
    static private int _max_count_press = 10;
    static private int _count_press = 10;
    static private int _timer = 500;


    void Awake()
    {
        _ad = transform.GetChild(0).gameObject;
        _present = gameObject.transform.GetChild(1).gameObject;
        _click_animate = GetComponent<Animation>();
        if (!_wolf_manager)
            _wolf_manager = GameObject.Find("WolfManager").GetComponent<WolfManager>();
    }
    public void CreatePresent()
    {
        _ad.SetActive(false);
        _present.SetActive(true);
        _count_press = 1;
        Active();
    }
    public void CreateAd()
    {
        _ad.SetActive(true);
        _present.SetActive(false);
        _count_press = Constants.max_wolf_click;
        _count_press -= (5 - GameManager.current_location) * 10;
        _count_press -= (int)(Constants.max_wolf_click * YandexGame.savesData.help_noob);
        _count_press -= (int)(Constants.max_wolf_click * (YandexGame.savesData.help_change * 1.30));
        if(_count_press <= 0)
        {
            _count_press = 5;
        }
        Active();
    }
    
    public void CreateSpecial()
    {
        _ad.SetActive(true);
        _present.SetActive(false);
        _count_press = 10;
        _max_count_press = _count_press;
        _timer = 500;
        StartCoroutine(WolfTimer());
    }
    
    private void Active()
    {
        _max_count_press = _count_press;
        _timer = (int)(Constants.wolf_timer * (1f + YandexGame.savesData.help_noob));
        StartCoroutine(WolfTimer());
    }
    IEnumerator WolfTimer()
    {
        while (_timer > 0)
        {
            _wolf_manager.PlayTick();
            _timer--;
            if (_timer == 0)
            {
                _wolf_manager.OutTimer();
                gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDisable()
    {
        _wolf_manager.WolfDisabled();
    }

    private void OnMouseDown()
    {
        _click_animate.Play();
        _count_press--;

        _wolf_manager.PlayHit(_count_press, _max_count_press);
        if (_count_press <= 0)
        {
            if (_ad.activeInHierarchy)
            {
                YandexGame.savesData.HelpNoobChange();
            }
            _wolf_manager.PlayScare();
            gameObject.SetActive(false);
        }
    }

    
}

using UnityEngine;
using YG;

public class WolfManager : MonoBehaviour
{
    [SerializeField] private AudioClip scare;
    [SerializeField] private AudioClip final_present;
    [SerializeField] private AudioClip final_ad;
    [SerializeField] private AudioClip[] hit = { };
    [SerializeField] private AudioClip[] awake = { };
    [SerializeField] private AudioClip tick;

    [SerializeField] private Animation _pain_animate;
    [SerializeField] private AudioSource _pain_source;
    [SerializeField] private GameObject _objects;
    [SerializeField] private GameObject _wolf_health_slider;
    [SerializeField] private GameObject _wolf_clock;
    [SerializeField] private short _spawn_timer = -1;  ////////////////////////////////////////
    
    public bool play_briefing_wolf = true;

    private GameObject[] _left_wolfs = { };
    private GameObject[] _right_wolfs = { };
    private AudioSource _audio_source;
    private Type _wolf_type;
    private bool _is_left = true;
    public void WolfDisabled()
    {
        _wolf_health_slider.SetActive(false);
        _wolf_clock.SetActive(false);
    }
    
    public void SetTimer(int delay = 0 )
    {
        if (delay <= 0)
            _spawn_timer = (short)Random.Range(30 + (GameManager.current_location * 10), 70 + (GameManager.current_location * 10));
        else
            _spawn_timer = (short)delay;
    }
    public enum Type
    {
        present = 0,
        ad = 1,
    };
    public void WolfMassiveUpdate(int current_location)
    {
        if (1 < current_location && current_location < 6)
        {
            GameObject location = _objects.transform.GetChild(current_location - 1).gameObject;
            GameObject folder = GameObject.Find("UI/world/background/objects/location_" + current_location + "/wolfs/left");
            if (folder != null)
            {
                int count = folder.transform.childCount;
                _left_wolfs = new GameObject[count];
                for (int i = 0; i < count; i++)
                {
                    _left_wolfs[i] = folder.transform.GetChild(i).gameObject;
                }
            }
            folder = GameObject.Find("UI/world/background/objects/location_" + current_location + "/wolfs/right");
            if (folder != null)
            {
                int count = folder.transform.childCount;
                _right_wolfs = new GameObject[count];
                for (int i = 0; i < count; i++)
                {
                    _right_wolfs[i] = folder.transform.GetChild(i).gameObject;
                }
            }
            if (GameManager.current_location > 1)
            {
                BriefingWolf();
                SetTimer();
            }
        }
    }




    public void SwithPBW()
    {
        this.play_briefing_wolf = false;
    }

    public void BriefingWolf()
    {
        if (GameManager.current_location == 2 && play_briefing_wolf)
        {
            _wolf_health_slider.SetActive(true);
            _wolf_clock.SetActive(true);
            _right_wolfs[0].SetActive(true);
            _wolf_type = Type.ad;
            _right_wolfs[0].GetComponent<Wolf>().CreateSpecial();
            PlaySound(awake[Random.Range(0, 2)]);
        }
    }

    public void SpawnWolf()
    {
        foreach (GameObject wolf in _right_wolfs)
            if (wolf.activeInHierarchy) return;
        foreach (GameObject wolf in _left_wolfs)
            if (wolf.activeInHierarchy) return;

        if (_is_left)
            Spawn(_right_wolfs[Random.Range(0, _right_wolfs.Length)]);
        else
            Spawn(_left_wolfs[Random.Range(0, _left_wolfs.Length)]);
    }
    public void SetIsLeft(bool _is_left)
    {
        this._is_left = _is_left;
    }
    public void OutTimer()
    {
        if (_wolf_type == Type.present)
        {
            PlaySound(final_present);
            GameManager.GetPresent();
        }
        else
        {
            PlaySound(final_ad);
            _pain_source.Play();
            _pain_animate.Play();
            GameManager.PlayAd();
        }
    }
    public void PlayTick()
    {
        PlaySound(tick);
    }
    public void PlayScare()
    {
        PlaySound(scare);
        _wolf_health_slider.SetActive(false);
        _wolf_clock.SetActive(false);
    }
    public void PlayHit(int count, int max_count)
    {
        PlaySound(hit[Random.Range(0, 2)]);
        _wolf_health_slider.GetComponent<WolfProgressbar>().UpdateProgressbar(count, max_count);
    }
    private void Awake()
    {
        _audio_source = GetComponent<AudioSource>();
        InvokeRepeating("WolfSpawn", 1, 1);
    }
    private void WolfSpawn()
    {
        if (_spawn_timer > 0)
        {
            _spawn_timer--;
            if (_spawn_timer == 0)
            {
                SpawnWolf();
                SetTimer();
            }
        }
    }
    private void Spawn(GameObject wolf, float change_type = 0f)
    {
        _wolf_health_slider.SetActive(true);
        _wolf_clock.SetActive(true);
        wolf.SetActive(true);
        if(change_type == 0)
        {
            change_type = Random.Range(0, 1.0f);
        }
        if (change_type + YandexGame.savesData.help_change / 2 > 0.8f)
        {
            _wolf_type = Type.present;
            wolf.GetComponent<Wolf>().CreatePresent();
        }
        else
        {
            _wolf_type = Type.ad;
            wolf.GetComponent<Wolf>().CreateAd();
        }
        PlaySound(awake[Random.Range(0, 2)]);
    }


    private void PlaySound(AudioClip clip)
    {
        _audio_source.PlayOneShot(clip, Random.Range(0.2f, 1));
    }
}

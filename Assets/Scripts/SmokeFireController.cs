using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class SmokeFireController : MonoBehaviour
{
    private ParticleSystem _fire_particle;
    [SerializeField] private GameObject[] _particle_system;
    private ParticleSystem _current_particle;
    
    private void Start()
    {
        _fire_particle = GetComponent<ParticleSystem>();
        _current_particle = _fire_particle;
        Swipe_UI.SwipeEvent.AddListener(SwithPauseAnimate);
    }

    private void SwithPauseAnimate()
    {
        if (Swipe_UI.Left)
        {
            Invoke("PlayAnimate", 1);
        }
        else
        {
            _current_particle.Pause();
            _fire_particle.Pause();
        }
    }

    private void PlayAnimate()
    {
        _current_particle.Play();
        _fire_particle.Play();
    }

    private void OnEnable()
    {
        SwithLocationSmoke(GameManager.current_location);
    }

    public void SwithLocationSmoke(int current_location)
    {
        
        if(!gameObject.activeInHierarchy) 
        {
            return;
        }
        foreach (var p in _particle_system) 
        { 
            if(p.name == current_location.ToString())
            {
                p.SetActive(true);
                _current_particle = p.GetComponent<ParticleSystem>();
            }
            else
                p.SetActive(false);
        }
    }
}

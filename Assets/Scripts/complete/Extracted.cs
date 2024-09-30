using UnityEngine;
using YG;

public class Extracted : MonoBehaviour
{
    [SerializeField] Constants.resource _type_extracted;
    [SerializeField] private AudioClip[] _audio_clip = { };
    [SerializeField] private AudioClip _audio_error;
    private int _length_audio_massive;
    private AudioSource _audioSource;
    private Animation _click_animate;
    

    private void Start()
    {

        _length_audio_massive = _audio_clip.Length;
        _click_animate = GetComponent<Animation>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        if (Constants.mobile == false)
            Send(Input.mousePosition);
    }
    public void Send(Vector3 coord) 
    {
        _click_animate.Play();
        GameManager.Extracted(_type_extracted, this, coord);
    }

    public void PlayExtractedSound(int number)
    {
        if (number > 0)
        {
            _audioSource.PlayOneShot(_audio_clip[Random.Range(0, _length_audio_massive)], Random.Range(0.2f, 1));
        }
        else
        {
            if(_audio_error != null)
            {
                _audioSource.PlayOneShot(_audio_error, 1);
            }  
        }

    }

}

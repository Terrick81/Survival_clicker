using UnityEngine;
using UnityEngine.Events;

public class Swipe_UI : MonoBehaviour
{
    private Animator _animation;
    private WolfManager _wolf_manager;
    public static UnityEvent SwipeEvent = new UnityEvent();
    public static bool Left = true;

    private void Start()
    {
        _wolf_manager = GameObject.Find("WolfManager").GetComponent<WolfManager>();
        _animation = GetComponent<Animator>();

    }

    public void OnClickButtonUp()
    {
        Left = false;
        SwipeEvent.Invoke();
        _animation.SetBool("IsTriggered", true);
        if (_wolf_manager != null )
        {
            _wolf_manager.SetIsLeft(false);
        }
    }

    public void OnClickButtonBack()
    {
        Left = true;
        SwipeEvent.Invoke();
        _animation.SetBool("IsTriggered", false);
        if (_wolf_manager != null)
            _wolf_manager.SetIsLeft(true);
    }
}

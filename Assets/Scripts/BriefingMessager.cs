using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using YG;

public class BriefingMessager : MonoBehaviour
{
    public static bool briefing_off = false;
    public static UnityEvent Open_shop = new UnityEvent();
    public static string caller_name;
    public static BriefingMessager caller;
    [SerializeField] bool _get_ms_OnDisable = false;
    [SerializeField] bool _get_ms_OnEnable = false;
    [SerializeField] bool _get_ms_OnClick = true;
    [SerializeField] int briefing_lvl = 150;
    static Briefing briefing_script;
    private void Start()
    {

        if (briefing_script == null)
        {
            if (GameObject.Find("briefing") != null)
                briefing_script = GameObject.Find("briefing").GetComponent<Briefing>();
            else
            {
                Destroy(this);
            }
        }     
    }


    public void Massage()
    {
        if (briefing_off)
        {
            DestroyThis();
        }
        else if(briefing_script.lvl_briefing > briefing_lvl)
        {
            DestroyThis();
        }
        else
        {
            caller = this;
            caller_name = gameObject.name;
            Open_shop.Invoke();
        }
    }
    private void OnDisable()
    {
        if (_get_ms_OnDisable)
            Massage();
    }
    private void OnEnable()
    {
        if (_get_ms_OnEnable)
            Massage();
    }

    private void OnMouseDown()
    {
        if (_get_ms_OnClick)
            Massage();
    }
    public void DestroyThis()
    {
        if (TryGetComponent<EventTrigger>(out EventTrigger component))
            Destroy(GetComponent<EventTrigger>());
        caller_name = "";
        caller = null;
        Destroy(this);
    }
}

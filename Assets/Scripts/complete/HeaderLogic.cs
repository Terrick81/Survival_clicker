using TMPro;
using UnityEngine;

public class HeaderLogic : MonoBehaviour
{
    [SerializeField] private GameObject _header_group;
    [SerializeField] private TextMeshProUGUI[] _header_text_passive;
    [SerializeField]  private TextMeshProUGUI[] _header_text_active;
    private GameObject[] _header_object;

    void Awake()
    {
        int _count = _header_group.transform.childCount;
        _header_object = new GameObject[_count];
        _header_text_active = new TextMeshProUGUI[_count];
        _header_text_passive = new TextMeshProUGUI[_count];
        for (int i = 0; i < _count; i++)
        {
            _header_object[i] = _header_group.transform.GetChild(i).gameObject;
            _header_text_active[i] = _header_object[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _header_text_passive[i] = _header_object[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        }
    }
    
    public void NewResoursesUpdate(int[] passive_profit)
    {
        if (GameManager.current_location < Constants.c_res)
        {
            for(int i = 0; i < Constants.c_res; i++)
            {
                if (i <= GameManager.current_location)
                    _header_object[i].SetActive(true);
                else
                    _header_object[i].SetActive(false);
            }
        }
        UpdatePassiveTextAll(passive_profit);
    }


    public void UpdateActiveTextAll(int[] active_profit )
    {
        for (int i = 0; i < GameManager.count_res; i++)
        {
            _header_text_active[i].text = Constants.ConvertNumber(active_profit[i]);
        }
    }

    public void UpdateActiveText(int cullumn, int value)
    {
        _header_text_active[cullumn].text = Constants.ConvertNumber(value);
    }

    public void UpdatePassiveTextAll(int[] passive_profit)
    {
        for (int i = 0; i < GameManager.count_res; i++)
        {
            _header_text_passive[i].text = "+" + Constants.ConvertNumber(passive_profit[i]);
        }
    }

}

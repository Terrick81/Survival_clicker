using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class CraftLabelLogic : MonoBehaviour
{
    
    [SerializeField] private GameObject _craft_layer;
    private GameObject _name_obj;
    private LocalizeStringEvent _name_text;
    private ProgressbarItemLogic _progress_bar_script;
    private LocalizeStringEvent _profit_text;
    private GameObject[] _profit_group_list;
    private TextMeshProUGUI[] _profit_group_text;
    private LocalizeStringEvent _lvl_item_text;
    private GameObject[] _items_group_list;
    private TextMeshProUGUI[] _items_group_text;
    private LocalizeStringEvent _lvl_up_button_text;
    private GameObject _wolf_text;
    private TableReference _table = "UI";
    private PrefabLogic _item;
    private GameObject _lvl_item;
    private GameObject _profit_group;
    private GameObject _price_group;
    private GameObject _lock;
    private Button _button;
    RectTransform _trfm_layer;
    static CraftLabelLogic _craft_label_logic;
    static GameObject[] tools;
    float _offset;


    private void Start()
    {
        
        GameObject _root = GameObject.Find("shop_root");
        _name_obj = _root.transform.GetChild(0).gameObject;
        _name_text = _root.transform.GetChild(0).GetComponent<LocalizeStringEvent>();
        _progress_bar_script = _root.transform.GetChild(1).GetComponent<ProgressbarItemLogic>();
        _profit_text = _root.transform.GetChild(2).GetComponent<LocalizeStringEvent>();
        _profit_group = _root.transform.GetChild(3).gameObject;
        _lvl_item = _root.transform.GetChild(4).gameObject;
        _lvl_item_text = _lvl_item.GetComponent<LocalizeStringEvent>();
        _price_group = _root.transform.GetChild(5).gameObject;
        GameObject btn = _root.transform.GetChild(6).gameObject;
        _lvl_up_button_text = btn.GetComponent<LocalizeStringEvent>();
        _button = btn.GetComponent<Button>();
        _lock = btn.transform.GetChild(1).gameObject;
        _wolf_text = _root.transform.GetChild(7).gameObject;

        int child_count = _profit_group.transform.childCount;
        _profit_group_list = new GameObject[child_count];
        _items_group_list = new GameObject[child_count];
        _profit_group_text = new TextMeshProUGUI[child_count];
        _items_group_text = new TextMeshProUGUI[child_count];

        for (int i = 0; i < child_count; i++)
        {
            _profit_group_list[i] = _profit_group.transform.GetChild(i).gameObject;
            _profit_group_text[i] = _profit_group_list[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _items_group_list[i] = _price_group.transform.GetChild(i).gameObject;
            _items_group_text[i] = _items_group_list[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
        InvokeRepeating("UpdateAll", 0.5f, 1);
    }
    public void IncludeObject(GameObject[] list)
    {
        tools = list;
        _trfm_layer = _craft_layer.GetComponent<RectTransform>();
        _offset = _trfm_layer.anchorMax.x - _trfm_layer.anchorMin.x;
        _craft_label_logic = gameObject.GetComponent<CraftLabelLogic>();
    }
    
    public static byte[] SaveLvlEquepment()
    {
        byte[] data = new byte[tools.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            data[i] = tools[i].GetComponent<PrefabLogic>().lvl_eqpm;

        }
        return data;
    }

    public static bool[] SaveIsDiedEquepment()
    {
        bool[] data = new bool[tools.Length];
        for (int i = 0; i < tools.Length; i++)
        {
            data[i] = tools[i].GetComponent<PrefabLogic>().is_died;
            //Debug.Log(tools[i].name + " |:| " + tools[i].GetComponent<PrefabLogic>().is_died);
        }
        return data;
    }


    public void CheckLocationObject()
    {
        foreach (GameObject tool in tools)
        {
            if (tool != null)
            {
                tool.SetActive(true);
                tool.GetComponent<PrefabLogic>().UpdateLocation();
            }
        }
    }
    public static void StaticSpawn(PrefabLogic item)
    {
        _craft_label_logic.Spawn(item);
    }
    private void Spawn(PrefabLogic item)
    {
        
        if(item == _item && _craft_layer.activeInHierarchy)
        {
            return;
        }
        _item = item;
        _craft_layer.SetActive(true);
        _craft_layer.GetComponent<Animation>().Play();
        UpdatePosition();
        _progress_bar_script.UpdateProgressbar(_item.lvl_eqpm, _item.object_new);
        UpdatePanel();
    }
    private void UpdateName()
    {
        _name_text.StringReference.SetReference(_table, _item.name);
        
        if (_item.lvl_eqpm == 10 && _item.object_new == null)
        {
            _name_obj.GetComponent<TextMeshProUGUI>().color = Constants.max_color;
        }
        else
        {
            _name_obj.GetComponent<TextMeshProUGUI>().color = UnityEngine.Color.white;
        }
    }
    private void DefaultType()
    {
        _profit_group.SetActive(true);
        _wolf_text.SetActive(false);
        if (_item.is_fixed)
            _profit_text.StringReference.SetReference(_table, "passive");
        else
            _profit_text.StringReference.SetReference(_table, "active");
        UpdateGroup(_profit_group_list, _profit_group_text, _item.profit);
    }
    private void WolfType()
    {
        _profit_text.StringReference.SetReference(_table, "additional_effects");
        _wolf_text.SetActive(true);
        _profit_group.SetActive(false);
    }
    private void MaxLvlType()
    {
        _profit_text.StringReference.SetReference(_table, "max_LVL");
        _profit_group.SetActive(false);
        _lvl_item.SetActive(false);
        _price_group.SetActive(false);
        _wolf_text.SetActive(false);
        _lock.SetActive(true);
    }
    private void CurrentMaxLvlType()
    {
        _profit_text.StringReference.SetReference(_table, "current_max_LVL");
        _profit_group.SetActive(false);
        _lvl_item.SetActive(false);
        _price_group.SetActive(false);
        _wolf_text.SetActive(false);
        _lock.SetActive(true);
    }
    private void UpdatePanel()
    {
        UpdateName();
        if (_item.lvl_eqpm == 10)
        {
            if (_item.object_new == null)
            {
                MaxLvlType();
                return;
            }
            else if (_item.object_new_complete == false)
            {
                CurrentMaxLvlType();
                return;
            }
        }
        _lock.SetActive(false);
        _lvl_item.SetActive(true);
        _price_group.SetActive(true);
        UpdateHeadingText();
        if (WolfCheck())
            WolfType();
        else
            DefaultType();
        UpdateGroup(_items_group_list, _items_group_text, _item.price);
        UpdateColor();
        UpdateButton();
        UpdateColor();
    }
    private void UpdateHeadingText()
    {
        if (_item.lvl_eqpm == 0)
        {
            _lvl_up_button_text.StringReference.SetReference(_table, "create_2");
            _lvl_item_text.StringReference.SetReference(_table, "create_1");
        }
        else
        {
            _lvl_up_button_text.StringReference.SetReference(_table, "lvl_up_2");
            _lvl_item_text.StringReference.SetReference(_table, "lvl_up_1");
        }
    }
    private bool WolfCheck()
    {
        foreach (string _scare_item in Constants.wolf_scare)
        {
            if (_item.name == _scare_item)
            {
                return true;
            }
        }
        return false;
    }
    private void UpdatePosition()
    {
        Vector2 _item_Max = _item.rect_transform.anchorMax;
        Vector2 _item_Min = _item.rect_transform.anchorMin;
        Vector2 _shop_Max = _trfm_layer.anchorMax;
        Vector2 _shop_Min = _trfm_layer.anchorMin;

        if (_item_Max.x > 0.5)
        {
            _trfm_layer.anchorMax = new Vector2(_item_Min.x - 0.05f, _shop_Max.y);
            _trfm_layer.anchorMin = new Vector2(_trfm_layer.anchorMax.x - _offset, _shop_Min.y);
        }
        else
        {

            _trfm_layer.anchorMax = new Vector2(_item_Max.x + _offset + 0.05f, _shop_Max.y);
            _trfm_layer.anchorMin = new Vector2(_item_Max.x + 0.05f, _shop_Min.y);
        }
    }
    private void UpdateGroup(GameObject[] _group, TextMeshProUGUI[] _text, int[] _check)
    {
        
        for (int i = 0; i < _group.Length; i++)
        {
            if (_check[i] == 0)
            {
                _group[i].SetActive(false);
            }
            else
            {
                _group[i].SetActive(true);
                _text[i].text = Constants.ConvertNumber(_check[i]);
            }
        }
    }
    private void UpdateColor()
    {
        bool[] g1 = new bool[Constants.c_res];
        g1 = GameManager.BuyEquipmentCheckMass(_item);
        for (int i = 0; i < GameManager.count_res; i++)
        {
            if (g1[i])
                _items_group_text[i].colorGradient = new VertexGradient(Color.white, Color.white, Color.white, Color.white);
            else
                _items_group_text[i].colorGradient = new VertexGradient(Color.white, Color.white, Constants.Pink, Constants.Pink);
        }
    }
    private void UpdateAll()
    {
        if (!_craft_layer.activeInHierarchy)
            return;
        if (_item == null)
            return;

        UpdateButton();
        UpdateColor();
    }
    private void UpdateButton()
    {
        if (_item.CheckBuy())
        {
            _button.interactable = true;
        }
        else
        {
            _button.interactable = false;
        }
    }   
    public void Buy()
    {
        if (_item.CheckBuy())
        {
            PrefabLogic item = _item;
            if (_item.LvlUpObject())
            {
                GameManager.BuyEquipment(item);
                _progress_bar_script.UpdateProgressbar(_item.lvl_eqpm, _item.object_new, lvlUp: true);
                UpdatePanel();
                UpdateButton();
                UpdateColor();
            }
        }
    }
}


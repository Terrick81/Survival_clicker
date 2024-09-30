using UnityEngine;
using YG;

public class GameManager : MonoBehaviour
{
    public enum localization
    {
        RU = 0,
        ENG = 1,
        TR = 2,
    };

    public static int[] resources = { 0, 0, 0, 0, 0, 0 };
    public static int[] fixed_extraction = { 0, 0, 0, 0, 0, 0 };
    public static int[] click_extraction = { 1, 0, 0, 1, 0, 0 };
    public static byte current_location = 1;
    public static byte count_res = (byte)(current_location + 1);
    public static float old_time = 0;

    static int _object_need;
    static int _object_buy;

    static TextInstantiate _extracted_text_script;
    static GameManager _game_manager_script;
    static GameObject _ad_logic;
    static GameObject _gift;
    static CraftLabelLogic _craft_label_logic;
    static LocationLogic _location_logic_script;
    static HeaderLogic _header_logic_ui_script;
    static WolfManager _wolf_manager_script;
    static ProgressbarLocationLogic _location_bar_script;
    static GameObject _lvl_up_location_button;

    private void Awake()
    {
        _ad_logic = GameObject.Find("UI/main/table");
        _ad_logic.SetActive(false);
        _lvl_up_location_button = GameObject.Find("location_up").gameObject;
        _lvl_up_location_button.SetActive(false);
        _location_bar_script = GameObject.Find("location_bar").GetComponent<ProgressbarLocationLogic>();
        _wolf_manager_script = GameObject.Find("WolfManager").GetComponent<WolfManager>();
        _gift = GameObject.Find("UI/main/gift");
        _extracted_text_script = GetComponent<TextInstantiate>();
        _game_manager_script = GetComponent<GameManager>();
        _craft_label_logic = GetComponent<CraftLabelLogic>();
        _header_logic_ui_script = GetComponent<HeaderLogic>();
        _location_logic_script = GameObject.Find("world").GetComponent<LocationLogic>();
        
    }
    
    private void Start()
    {
        _craft_label_logic.IncludeObject(GetComponent<ReadCSVData>().Read());
        _header_logic_ui_script.UpdateActiveTextAll(resources);
        _header_logic_ui_script.UpdatePassiveTextAll(fixed_extraction);
        InvokeRepeating("UpdateResources", 1, 1);
        UpdateLocation();
    }
    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GetComponent<SaveData>().RestartProgress();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetComponent<GameManager>().LocationLvlUp();
        }
    }
    */
    public static void AddToResources(int number, int i)
    {
        
        if (resources[i] + number  < Constants.max_value)
        {
            if(resources[i] + number >= 0)
            {
                resources[i] += number;
            }
        }
        else
        {
            resources[i] = Constants.max_value;
        }
    }

    private void UpdateResources()
    {
        for (int i = 0; i < GameManager.count_res; i++)
        {
            AddToResources(fixed_extraction[i], i);
        }
        _header_logic_ui_script.UpdateActiveTextAll(resources);
    }

    
    public static void Extracted(Constants.resource _type_extracted, Extracted _extract, Vector3 coord)
    {
        int type = (int)_type_extracted;
        _extracted_text_script.CreateText(click_extraction[type], coord);
        _extract.PlayExtractedSound(click_extraction[type]);
        AddToResources(click_extraction[type], type);
        _header_logic_ui_script.UpdateActiveText(type, resources[type]);
    }

    public static bool BuyEquipmentCheck(PrefabLogic item)
    {
        for (int i = 0; i < GameManager.count_res; i++)
        {
            if (resources[i] < item.price[i])
            {
               return false;
            }
        }
        return true;
        
    }
    public static bool[] BuyEquipmentCheckMass(PrefabLogic item)
    {
        bool[] g = new bool[GameManager.count_res];
        for (int i = 0; i < GameManager.count_res; i++)
        {
            if (resources[i] < item.price[i])
                g[i] = false;
            else
                g[i] = true;   
        }
        return g;
    }

    public static void BuyEquipment(PrefabLogic item)
    {
        for (int i = 0; i < GameManager.count_res; i++)
        {
            AddToResources(-item.price[i], i);
            item.SetPrice((int)(item.price[i] * Constants.price_scale), i);
            if (item.is_fixed)
                fixed_extraction[i] += item.profit[i];
            else
                click_extraction[i] += item.profit[i];
        }
        if (item.lvl_eqpm == Constants.next_lvl)
        {
            _object_buy++;
            CheckLvlUpUpdate();
        }
        foreach (string _scare_item in Constants.wolf_scare)
        {
            if (item.name == _scare_item)
            {
                YandexGame.savesData.help_change += Constants.help_change_get;
            }
        }
        _header_logic_ui_script.UpdateActiveTextAll(resources);
        _header_logic_ui_script.UpdatePassiveTextAll(fixed_extraction);
    }

    public static void CheckLvlUpUpdate()
    {
        _location_bar_script.UpdateProgressbar(_object_buy, _object_need);
        if(_object_buy >= _object_need)
        {
            _lvl_up_location_button.SetActive(true);
        }
    }



    public static float GetTime()
    {
        var delta_time = Time.time - old_time;
        old_time = Time.time;
        return delta_time;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LocationLvlUp()
    {
        current_location += 1;
        _location_logic_script.PlayWalk();
        Invoke("UpdateLocation", 1.3f);

    }
    public void UpdateLocation()
    {
        SaveData.SaveTime();
        if (current_location + 1 <= Constants.c_res)
            count_res = (byte)(current_location + 1);
        _object_buy = 0;
        _object_need = 0;
        _craft_label_logic.CheckLocationObject();
        _location_bar_script.UpdateLocation();
        CheckLvlUpUpdate();
        _location_logic_script.ChangeLocation(current_location);
        _wolf_manager_script.WolfMassiveUpdate(current_location);
        _header_logic_ui_script.NewResoursesUpdate(fixed_extraction);
        SaveData.SaveDataFunc();
        YandexGame.FullscreenShow();
    }

    public static void SetObjectNeed()
    {
        _object_need++;
    }
    public static void SetObjectBuy()
    {
        _object_buy++;
    }
    public static void PlayAd()
    {
        _ad_logic.SetActive(true);
    }
    public static void GetBuyNeed()
    {
        Debug.Log("buy: " + _object_buy);
        Debug.Log("need: " + _object_need);
    }
    public static void GetPresent()
    {
        int type_resurses = Random.Range(0, current_location);
        while (click_extraction[type_resurses] == 0)
        {
            type_resurses = Random.Range(0, current_location); 
        }
        resources[type_resurses] += click_extraction[type_resurses] * Constants.gift_scale;
        _gift.SetActive(true);
        _gift.GetComponent<Gift>().SetText(type_resurses, click_extraction[type_resurses] * Constants.gift_scale);
    }

}

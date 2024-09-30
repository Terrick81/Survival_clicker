using UnityEngine;
using UnityEngine.UI;

public class PrefabLogic : MonoBehaviour
{
    static readonly Color _new_color = new Color(0, 0, 0, 0.941f);
    private Animator _animator;
    private GameObject _message_simvol;
    public RectTransform rect_transform;
    public bool is_fixed = false;
    public int[] profit = { 0, 0, 0, 0, 0, 0 };
    public int[] price = { 0, 0, 0, 0, 0, 0 };
    public byte lvl_eqpm = 1;
    public byte lvl_location = 1;
    public GameObject object_new = null;
    public GameObject object_old = null;
    public bool object_new_complete = false;
    public bool is_died = false;
    private GameObject fire_system;

    private void Awake()
    {
        if(transform.childCount > 2)
        {
            fire_system = transform.GetChild(2).gameObject;
        }
        _message_simvol = transform.GetChild(1).gameObject;
        _animator = GetComponent<Animator>();
        rect_transform = GetComponent<RectTransform>();
    }

    public void Load()
    {
        for (int i = 0; i < lvl_eqpm; i++)
        {
            for (int j = 0; j < price.Length; j++)
            {
                SetPrice((int)(price[j] * Constants.price_scale),j );
            }
        }
    }


    public void SetPrice(int localPrice,int i)
    {
        if (localPrice > Constants.max_value) localPrice = Constants.max_value;
        if (localPrice < 0) localPrice = 0;
        price[i] = localPrice;
    }
    public void UpdateLocation()
    {
        if (is_died == true)
        {
            gameObject.SetActive(false);
            return;
        }
        if (lvl_location == GameManager.current_location)
        {
            if (lvl_eqpm >= Constants.next_lvl)
                GameManager.SetObjectBuy();   
            GameManager.SetObjectNeed();
            if (lvl_eqpm == 0)
                GetComponent<Image>().color = _new_color;
            else
                TextOff();

            if (gameObject.name == "stone_pickaxe") Debug.Log("пришел1!");
            if (object_old == null)
            {
                if (lvl_eqpm > 0)
                    TextOff();
                return;
            }
            else
            {
                PrefabLogic old = object_old.GetComponent<PrefabLogic>();
                if (old.is_died == true)
                {
                    if (lvl_eqpm > 0)
                        TextOff();
                    return;
                }
            }
        }
        if (lvl_location < GameManager.current_location)
        {
            if (lvl_eqpm > 0 )
            {
                TextOff();
                if (object_new != null)
                {
                    PrefabLogic new_obj = object_new.GetComponent<PrefabLogic>();
                    if (new_obj.CheckNewObject())
                    {
                        object_new_complete = true;
                        CreateMessage();
                        return;
                    }
                }

            }
            return;
        }
        gameObject.SetActive(false);
    }

    public void TextOff()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void CreateMessage()
    {
        _message_simvol.SetActive(true);
    }


    public bool CheckBuy()
    {
        if (GameManager.BuyEquipmentCheck(this))
        {
            if (lvl_eqpm < 10)
            {
                return true;
            }
            else
            {
                if (object_new_complete)
                {
                    return true;
                }
            }
        }
        return false;
    }


    public bool LvlUpObject() //возвращет успешно при проведении операции
    {
        if(object_new != null)
        {
            PrefabLogic script = object_new.GetComponent<PrefabLogic>();
            if (script.CheckNewObject())
            {
                object_new_complete = true;
            }
        }
        if (lvl_eqpm < 10)
        {
            lvl_eqpm++;
            GetComponent<Image>().color = Color.white;
            if(fire_system != null)
            {
                fire_system.SetActive(true);
            }
            transform.GetChild(0).gameObject.SetActive(false);
            //Debug.Log(gameObject.name + ": получил повышение до " + lvl_eqpm + " уровня!");
            return true;
        }
        else
        {
            if (object_new)
            {
                object_new.SetActive(true);
                if (object_new.GetComponent<PrefabLogic>().LvlUp())
                {
                    //Debug.Log(gameObject.name + ": заспавнил друга");
                    object_new.GetComponent<PrefabLogic>().SpawnPanel();
                    gameObject.SetActive(false);
                    is_died = true;                       
                    return true;
                }
            }
        }
        //Debug.Log(gameObject.name + ": пока что получил максимальный уровнь!");
        return false;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    public bool CheckNewObject()
    {
        if (lvl_location == GameManager.current_location)  ////////////////////////////////////
        {
            return true;
        }
        return false;
    }
    public bool LvlUp()
    {
        if (GameManager.current_location == lvl_location)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            return true;
        }
        gameObject.SetActive(false);
        return false;
    }


    private void OnMouseEnter()
    {
        _animator.SetTrigger("Highlighted");
    }

    private void OnMouseDown()
    {
        _animator.SetTrigger("Pressed");
        SpawnPanel();
    }


    private void SpawnPanel()
    {
        CraftLabelLogic.StaticSpawn(this);
        if (!object_new_complete)
        {
            _message_simvol.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        _animator.SetTrigger("Normal");
    }

}

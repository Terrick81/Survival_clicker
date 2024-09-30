using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Briefing : MonoBehaviour
{
    [SerializeField] GameObject _right_button;
    [SerializeField] GameObject _shop;
    [SerializeField] WolfManager _wolfManager;
    TipLogic _tip_logic;
    public int lvl_briefing = 0;
    bool load = true;
    Dictionary<int, string> dict = new Dictionary<int, string>
    {
        {0, "0" },
        {1, "tip" },
        {2, "WolfSpawner" },
    };


    GameObject[] _massages;
    int wood_count = 0;
    bool wood_event = false;

    private void Awake()
    {
        
        _tip_logic = gameObject.GetComponent<TipLogic>();
        _massages = new GameObject[gameObject.transform.childCount];
        for (int i = 0; i < _massages.Length; i++)
        {
            _massages[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void Load()
    {
        if (GameObject.Find("briefing") != null)
        {
            wood_count = YandexGame.savesData.resources[0];
            BriefingMessager.Open_shop.AddListener(Swith);
            
            if (lvl_briefing <= 2)
                Swith();
            load = false;
        }
    }
    public void DestroyBriefing()
    {
        YandexGame.savesData.briefing_off = true;
        BriefingMessager.briefing_off = true;
        _right_button.SetActive(true);
        Invoke("DestroyObject", 0.4f);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void Swith()
    {
        string message;
        if (load)
            message = dict[lvl_briefing];
        else
            message = BriefingMessager.caller_name;
        switch (message)
        {
            case "0":
                Debug.Log(_massages[0]);
                _massages[0].SetActive(true);
                _right_button.SetActive(false);
                return;
            case "digging_stick":
                if(!wood_event)
                {
                    _right_button.SetActive(true);
                    _massages[2].SetActive(true);
                    _massages[3].SetActive(true);
                }
                else
                {
                    _massages[5].SetActive(true);
                    DestroyMessanger();
                }
                _massages[1].SetActive(false);
                return;
            
            case "wood":
                wood_count++;
                if (wood_count >= 10)
                {
                    _massages[2].SetActive(false);
                    _massages[3].SetActive(false);
                    _massages[4].SetActive(true);
                    wood_event = true;  
                    DestroyMessanger();
                }
                return;

            case "shop_layer":
                MarkerFromShop();
                return;
            case "upper_button":
                if (wood_event)
                {
                    _massages[4].SetActive(false);
                    _tip_logic.PlayTip(1);
                    _tip_logic.PlayTip(4, delay: 50);
                    DestroyMessanger();
                    FindDestroyUnit("shop_layer");
                    FindDestroyUnit("left_button");
                    FindDestroyUnit("digging_stick");
                    _massages[5].SetActive(false);
                    lvl_briefing = 1;

                }
                return;
            case "tip":
                    _tip_logic.PlayTip(1, delay: 4);
                    _tip_logic.PlayTip(4, delay: 50);
                    lvl_briefing = 2;
                return;

            case "left_button":
                MarkerFromShop();
                return;
            
            case "knife":
                _tip_logic.PlayTip(3);
                DestroyMessanger();
                lvl_briefing = 4;
                return;
            
            case "location_up_btn":
                if(GameManager.current_location == 2)
                    Invoke("LocationUpBtn", 3);
                return;
            case "WolfSpawner":
                if (GameManager.current_location == 2)
                    LocationUpBtn();
                return;
            case "wolf_button_yes":
                Time.timeScale = 1.0f;
                _massages[7].SetActive(true);
                _massages[8].SetActive(true);
                DestroyMessanger();
                return;

            case "wolf (2)":
                _massages[7].SetActive(false);
                _massages[8].SetActive(false);
                _tip_logic.PlayTip(2);
                _wolfManager.SetTimer();
                DestroyMessanger();
                lvl_briefing = 3;
                return;

            case "chainsaw":
                _tip_logic.PlayTip(5);
                DestroyMessanger();
                lvl_briefing = 5;
                return;
        }
    }

    private void LocationUpBtn()
    {
        Time.timeScale = 0.0f;
        _massages[6].SetActive(true);
        DestroyMessanger();
    }

    private void  DestroyMessanger()
    {
        if(BriefingMessager.caller != null)
        {
            BriefingMessager.caller.DestroyThis();
        }
    }

    private void FindDestroyUnit(string name)
    {
        GameObject g = GameObject.Find(name);
        Debug.Log(name + g);
        if(g != null) 
        {
            if (g.TryGetComponent(out BriefingMessager briefing_messager))
                briefing_messager.GetComponent<BriefingMessager>().DestroyThis();
        }
        
    }

    private void MarkerFromShop()
    {
        if (wood_event)
        {
            if (_shop.activeInHierarchy)
            {
                _massages[1].SetActive(false);
                _massages[5].SetActive(true);
            }
            else
            {
                _massages[1].SetActive(true);
                _massages[5].SetActive(false);
            }
        }
    }

}

using System;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class ReadCSVData : MonoBehaviour
{
    [SerializeField] private TextAsset _text_asset_data;

    private const int _count_column = 18;
    public static byte[] load_lvl_eqpm_arr;
    public GameObject[] Read()
    {
        if (_text_asset_data != null)
        {
            string[] data = _text_asset_data.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);
            int table_size = data.Length / _count_column - 1;
            GameObject[] _tools = new GameObject[table_size];
            int count = _count_column;
            int count_element = 0;
            for (int i = 0; i < table_size; i++)
            {
                _tools[i] = GameObject.Find("UI/world/objects scene/" + data[count]);
                PrefabLogic tool_script = _tools[i].GetComponent<PrefabLogic>();
                tool_script.is_fixed = Convert.ToBoolean(data[count + 1]);

                for (int number = 0; number < Constants.c_res; number++)
                {
                    tool_script.profit[number] = Convert.ToInt32(data[count + number + 2]);
                }
                for (int number = 0; number < Constants.c_res; number++)
                {
                    tool_script.price[number] = Convert.ToInt32(data[count + number + 8]);
                }
                if (YandexGame.savesData.no_reset)
                    tool_script.lvl_eqpm = load_lvl_eqpm_arr[count_element];                
                else
                    tool_script.lvl_eqpm = Convert.ToByte(data[count + 14]);
                tool_script.object_new = GameObject.Find("UI/world/objects scene/" + data[count + 15]);
                tool_script.object_old = GameObject.Find("UI/world/objects scene/" + data[count + 16]);
                tool_script.lvl_location = Convert.ToByte(data[count + 17]);
                if (YandexGame.savesData.no_reset)
                    tool_script.is_died = YandexGame.savesData.load_is_died_arr[count_element];
                else
                    tool_script.is_died = false;
                Debug.Log(tool_script.name + ": " + tool_script.is_died);
                count += _count_column;
                count_element++;
                tool_script.Load(); 
            }   
            YandexGame.savesData.no_reset = true;
            return _tools;
        }
        return null;
    }
}

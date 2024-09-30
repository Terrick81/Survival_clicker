using System;
using UnityEngine;
using YG;
using static UnityEngine.AudioSettings;

public class Constants : MonoBehaviour
{
    public const String yandex_LB_name = "TimeToComplete";
    public const float help_change_get = 0.02f;
    public const int next_lvl = 5;
    public const int max_lvl = 10;
    public const int   c_res = 6;
    public const float price_scale = 1.20f;
    public const byte wolf_timer = 10;
    public const sbyte ad_timer = 7;
    public const short gift_scale = 500;
    public const int max_value = 2000000000;
    public static string[] wolf_scare = { "knife", "pet", "mini_house" };
    public static Color disable_color = new(0.76f, 0.76f, 0.76f);
    public static Color max_color = new(1f, 0.98f, 0.66f);
    public static Color Pink = new(1f, 0.54f, 0.59f);
    public static int count_elm = 34;
    public static byte max_wolf_click = 90;
    public static bool mobile = false;
    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                mobile = true;
                max_wolf_click = 90;
            }
            else
            {
                mobile = false;
                max_wolf_click = 140;
            }
        }
    }
    public enum resource
    {
        wood = 0,
        stone = 1,
        fish = 2,
        iron = 3,
        fur = 4,
        coal = 5,
    };


    public static string ConvertNumber(int value)
    {
        String vt;
        if (value >= 1000000000)
        {
            vt = String.Format("{0:0.000}", value / 1000000000.0f);
            return vt.Remove(4).TrimEnd(',') + "T";
        }
        if (value >= 1000000)
        {
            vt = String.Format("{0:0.000}", value / 1000000.0f);
            return vt.Remove(4).TrimEnd(',') + "M";
        }
        if (value >= 1000)
        {
            vt = String.Format("{0:0.000}", value / 1000.0f);
            return vt.Remove(4).TrimEnd(',') + "K";
        }
        return value.ToString();
    }
}

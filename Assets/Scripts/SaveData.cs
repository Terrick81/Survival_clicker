using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SaveData : MonoBehaviour
{
    [SerializeField] GameObject text;
    const float interval_autosave = 30f;
    static SaveData save_data_script;
    static Briefing briefing_script;

    private void Awake()
    {
        save_data_script = GetComponent<SaveData>();
        briefing_script = GameObject.Find("briefing").GetComponent<Briefing>();

    }
    private void Start()
    {
        if (YandexGame.SDKEnabled)
        {
            LoadData();
            StartCoroutine(AutoSave());
        }
    }

    public void OnEnable() => YandexGame.GetDataEvent += LoadData;
    public void OnDisable() => YandexGame.GetDataEvent -= LoadData;

    public void LoadData()
    {
        GameManager.resources = YandexGame.savesData.resources;
        GameManager.fixed_extraction = YandexGame.savesData.fixed_extraction;
        GameManager.click_extraction = YandexGame.savesData.click_extraction;
        GameManager.current_location = YandexGame.savesData.current_location;
        LocalizationButton.current = YandexGame.savesData.current_language;
        var b = YandexGame.savesData.play_briefing_wolf;
        GameObject.Find("WolfManager").GetComponent<WolfManager>().play_briefing_wolf = b;
        BriefingMessager.briefing_off = YandexGame.savesData.briefing_off;
        if (YandexGame.savesData.briefing_off)
        {
            briefing_script.DestroyBriefing();
        }
        else
        {
            briefing_script.lvl_briefing = YandexGame.savesData.lvl_briefing;
            briefing_script.Load();
        }
        if (YandexGame.savesData.load_lvl_eqpm_arr != null)
        {
            if (YandexGame.savesData.load_lvl_eqpm_arr[0] == 10)
            {
                ReadCSVData.load_lvl_eqpm_arr = YandexGame.savesData.load_lvl_eqpm_arr;
            }
        }
    }

    public static void SaveDataFunc()
    {
        save_data_script.SaveText();
        YandexGame.savesData.resources = GameManager.resources;
        YandexGame.savesData.fixed_extraction = GameManager.fixed_extraction;
        YandexGame.savesData.click_extraction = GameManager.click_extraction;
        YandexGame.savesData.current_location = GameManager.current_location;
        YandexGame.savesData.load_lvl_eqpm_arr = CraftLabelLogic.SaveLvlEquepment();
        YandexGame.savesData.load_is_died_arr = CraftLabelLogic.SaveIsDiedEquepment();
        YandexGame.savesData.current_language = LocalizationButton.current;
        YandexGame.savesData.lvl_briefing = briefing_script.lvl_briefing;
        var b = GameObject.Find("WolfManager").GetComponent<WolfManager>().play_briefing_wolf;
        YandexGame.savesData.play_briefing_wolf = b;
        
        SaveTime();
        YandexGame.SaveProgress();
        Debug.Log("help_change: " + YandexGame.savesData.help_change);
        Debug.Log("help_noob: " + YandexGame.savesData.help_noob);
        Debug.Log("lvl_briefing: " + YandexGame.savesData.lvl_briefing);
        Debug.Log("play_briefing_wolf: " + YandexGame.savesData.play_briefing_wolf);
    }


    public static void SaveTime()
    {
        if (GameManager.current_location != 6)
        {
            YandexGame.savesData.timeToComplete = YandexGame.savesData.timeToComplete + GameManager.GetTime();
        }
    }

    public void RestartProgress()
    {
        YandexGame.ResetSaveProgress();
        BriefingMessager.briefing_off = false;
        SaveDataFunc();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnApplicationQuit()
    {
        SaveDataFunc();
    }

    IEnumerator AutoSave()
    {
        while(true)
        {
            SaveDataFunc();
            yield return new WaitForSeconds(interval_autosave);
        }
    }
    public void  SaveText()
    {
        text.SetActive(true);
        Invoke("N1s", 3f);
    }

    private void N1s()
    {
        text.SetActive(false);
    }
}


using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)

        // Ваши сохранения

        public int[] resources = { 0, 0, 0, 0, 0, 0 };
        public int[] fixed_extraction = { 0, 0, 0, 0, 0, 0 };
        public int[] click_extraction = { 1, 0, 0, 1, 0, 0 };
        public byte current_location = 1;
        public byte[] load_lvl_eqpm_arr = new byte[Constants.count_elm];
        public bool[] load_is_died_arr = new bool[Constants.count_elm];
        public bool no_reset = false;
        public float help_change = 0.0f;
        public float help_noob = 0.6f;
        public int current_language = 1;
        public float timeToComplete = 0f;
        public float better_time = float.MaxValue;
        public bool briefing_off = false;
        public int lvl_briefing = 0;
        public bool play_briefing_wolf = true;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        public void HelpNoobChange()
        {
            if (help_noob > 0)
                help_noob -= 0.1f;
            else
            {
                help_noob = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PriceGame : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("RateGame", 10);
    }
    private void RateGame()
    {
        if (YandexGame.EnvironmentData.reviewCanShow)
        {
            YandexGame.ReviewShow(YandexGame.auth);
        }
    }
}

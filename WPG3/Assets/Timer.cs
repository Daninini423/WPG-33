using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] float remainingTime;
    [SerializeField] GameOverManager gameOverManager;
    // Update is called once per frame

    bool isGameOverTriggered = false;
    void Update()
    {
        if (isGameOverTriggered) return; // jangan jalan lagi kalau sudah game over

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0) remainingTime = 0; // biar ga negatif
        }
    
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0 && !isGameOverTriggered)
        {
            timertext.color = Color.red;
            isGameOverTriggered = true;

            if (gameOverManager != null)
                gameOverManager.ShowGameOver();
        }
    }
}

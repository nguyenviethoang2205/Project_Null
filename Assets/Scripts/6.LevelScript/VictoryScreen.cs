﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class VictoryScreen : MonoBehaviour
{
    public bool isVictory = false;
    public Button victoryMap;
    public Text victoryMapText;
    public Text victoryText;
    public Image victoryPanel;
    public Boards boards;
    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        StartCoroutine(VictoryAnimation());
        isVictory = true;
    }

    public void ExitToMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Map");
    }

    public void ExitGame(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator VictoryAnimation()
    {
        Color panelColor = victoryPanel.color;
        float deltaAlpha = 0.01f;
        victoryPanel.gameObject.SetActive(true);
        //Hiện Panel      
        while (panelColor.a < 0.7f)
        {
            panelColor.a += deltaAlpha;
            victoryPanel.color = panelColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        // Hiện Text
        Color textColor = victoryText.color;
        deltaAlpha = 0.01f;
        while (textColor.a < 1f)
        {
            textColor.a += deltaAlpha;
            victoryText.color = textColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        // Hiện button
        Color buttonColor = victoryMap.image.color;

        deltaAlpha = 0.01f;
        while (buttonColor.a < 1f)
        {
            buttonColor.a += deltaAlpha;
            victoryMap.image.color = buttonColor;
            victoryMapText.color = buttonColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
    }
}

﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public bool isOver = false;
    public Button gameOverRestart;
    public Button gameOverExit;
    public Text gameOverRestartText;
    public Text gameOverExitText;
    public Text gameOverText;
    public Image gameOverPanel;
    public Boards boards;
    public Button button;
    public GameObject restartButton;
    public Character character;

    private void Awake() {
        
        restartButton = GameObject.Find("RestartButton");
        button = restartButton.GetComponentInChildren<Button>();
        GameObject player = GameObject.FindWithTag("Player");
        Debug.Log("Nhan "+player);
        character = player.GetComponentInChildren<Character>();
    }

    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        StartCoroutine(GameOverAnimation());
        isOver = true;
        if(character.monsterTrophy < 1){
            button.interactable = false;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tetris");
    }

    public void Restart2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tetris_2");
    }

    public void Restart3()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Tetris_3");
    }

    public void Restart4()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Tetris_4");
    }

    public void RestartBoss()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Tetris_Boss");
    }

    public void RestartElite()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tetris_Elite");
    }

    public void RestartElite2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tetris_Elite_2");
    }


    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator GameOverAnimation()
    {
        Color panelColor = gameOverPanel.color;
        float deltaAlpha = 0.01f;
        gameOverPanel.gameObject.SetActive(true);
        //Hiện Panel      
        while (panelColor.a < 0.7f)
        {
            panelColor.a += deltaAlpha;
            gameOverPanel.color = panelColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        // Hiện Text
        Color textColor = gameOverText.color;
        deltaAlpha = 0.01f;
        while (textColor.a < 1f)
        {
            textColor.a += deltaAlpha;
            gameOverText.color = textColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        // Hiện button
        Color buttonColor = gameOverRestart.image.color;
        deltaAlpha = 0.01f;
        while (buttonColor.a < 1f)
        {
            buttonColor.a += deltaAlpha;
            gameOverRestart.image.color = buttonColor;
            gameOverRestartText.color = buttonColor;
            gameOverExit.image.color = buttonColor;
            gameOverExitText.color = buttonColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
    }
}
﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour{
    public bool isVictory = false;
    public void Setup(){
        Time.timeScale = 0f;
        gameObject.SetActive(true); 
        isVictory = true;
    }

    public void ExitToMap(){
        SceneManager.LoadScene("MainMenu");
    }
}

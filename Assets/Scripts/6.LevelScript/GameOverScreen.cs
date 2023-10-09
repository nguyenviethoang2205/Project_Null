using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour{
    public bool isOver = false;
    public void Setup(){
        Time.timeScale = 0f;
        gameObject.SetActive(true); 
        isOver = true;
    }

    public void Restart(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tetris");
    }

    public void ReturnMenu(){
        SceneManager.LoadScene("MainMenu");
    }

}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour{
    public bool isOver = false;
    public Button gameOverRestart;
    public Button gameOverExit;
    public Text gameOverRestartText;
    public Text gameOverExitText;
    public Text gameOverText;
    public Image gameOverPanel;
    public Boards boards;
    
    public void Setup(){
        Time.timeScale = 0f;
        gameObject.SetActive(true); 
        StartCoroutine(GameOverAnimation());
        isOver = true;
        
    }

    public void Restart(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris");
        SceneManager.LoadScene("Tetris", LoadSceneMode.Additive);
    }

    public void Restart2(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_2");
        SceneManager.LoadScene("Tetris_2", LoadSceneMode.Additive);
    }

    public void Restart3(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_3");
        SceneManager.LoadScene("Tetris_3", LoadSceneMode.Additive);
    }

    public void Restart4(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_4");
        SceneManager.LoadScene("Tetris_4", LoadSceneMode.Additive);
    }

    public void RestartBoss(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_Boss");
        SceneManager.LoadScene("Tetris_Boss", LoadSceneMode.Additive);
    }

    public void RestartElite(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_Elite");
        SceneManager.LoadScene("Tetris_Elite", LoadSceneMode.Additive);
    }

    public void RestartElite2(){
        Time.timeScale = 1f;
        boards.DestroyPlayer();
        SceneManager.UnloadScene("Tetris_Elite_2");
        SceneManager.LoadScene("Tetris_Elite_2", LoadSceneMode.Additive);
    }


    public void ReturnMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator GameOverAnimation(){
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
            gameOverExit.image.color  = buttonColor;
            gameOverExitText.color = buttonColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
    }
}
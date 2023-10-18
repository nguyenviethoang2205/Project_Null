using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public bool isPause = false;

    public void Setup(){
        Time.timeScale = 0f;
        gameObject.SetActive(true); 
        isPause = true;
    }

    public void Continue(){
        Time.timeScale = 1f;
        gameObject.SetActive(false); 
        isPause = false;
    }

    public void ReturnMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnMenuElite(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnMenuBoss(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

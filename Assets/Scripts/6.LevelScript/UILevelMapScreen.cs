using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UILevelMapScreen : MonoBehaviour
{
    public bool isPause = false;
    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        isPause = true;
    }

    public void ReturnLevelMap() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        isPause = false;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPause = false;
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UILevelMapScreen : MonoBehaviour
{
    [SerializeField] private Path path;

    public void Setup()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        path.isPause = true;
    }

    public void ReturnLevelMap() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        path.isPause = false;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        path.isPause = false;
    }
}
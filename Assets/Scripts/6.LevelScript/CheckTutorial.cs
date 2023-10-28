using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckTutorial : MonoBehaviour
{
    [SerializeField] private Path path;
    public void Setup(){
        Time.timeScale = 0f;
        gameObject.SetActive(true); 
        path.isPause = true;
    }

    public void Continue(){
        Time.timeScale = 1f;
        gameObject.SetActive(false); 
        path.isPause = false;
    }
}

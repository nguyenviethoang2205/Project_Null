using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public Image GameLogo;
    public Button playButton;
    public Button exitButton;
    public Text fadeText;
    public Image openPanel; 
    public Image closePanel; 
    public AudioManager audioManager;

    public bool isDone = false;
    public bool isPlay = false;

    private void Update(){
        if (Input.GetMouseButtonDown(0) & isDone == false & isPlay == false){
            audioManager.PlayLoopSound();
            StartCoroutine(MoveLogo());
        }
    }
    
    public void PlayGame(){
        StartCoroutine(StartGame());
    }

    public void QuitGame(){
        StartCoroutine(ExitGame());
    }

   private IEnumerator ExitGame(){
        StartCoroutine(ShowPanel());
        yield return new WaitForSecondsRealtime(1f);
        Application.Quit();
    }


    private IEnumerator StartGame(){
        StartCoroutine(ShowPanel());
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("Level_Map");
    }

    // Di chuyển logo
    private IEnumerator MoveLogo(){
        isPlay = true;
        StartCoroutine(HideText());
        bool isComing = false; 
        float y = 0f;
        Vector3 startPosition = GameLogo.transform.position;
        while (isComing == false){
            y += 0.03f;
            GameLogo.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            yield return new WaitForSecondsRealtime(0.01f);
            if (y >= 3f){
                isComing = true;
            }
        }
        StartCoroutine(HidePanel());
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(MovePlayButton());
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(MoveExitButton());
        isDone = true; 
    }

    private IEnumerator MoveExitButton(){
        bool isComing = false; 
        float y = -9f;
        Vector3 startPosition = exitButton.transform.position;
        while (isComing == false){
            y += 0.2f;
            exitButton.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            yield return new WaitForSecondsRealtime(0.01f);
            if (y >= -4f){
                isComing = true;
            }
        }
    }
    
    private IEnumerator MovePlayButton(){
        bool isComing = false; 
        float y = -7f;
        Vector3 startPosition = playButton.transform.position;
        while (isComing == false){
            y += 0.2f;
            playButton.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            yield return new WaitForSecondsRealtime(0.01f);
            if (y >= -2f){
                isComing = true;
            }
        }
    }

    private IEnumerator HideText(){
        Color textColor = fadeText.color;
        float deltaAlpha = 0.01f;
        while (textColor.a > 0)
        {
            textColor.a -= deltaAlpha;
            fadeText.color = textColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        
        fadeText.enabled = false;
    }

    private IEnumerator HidePanel()
    {
        Color panelColor = openPanel.color;
        float deltaAlpha = 0.01f;
        while (panelColor.a > 0)
        {
            panelColor.a -= deltaAlpha;
            openPanel.color = panelColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
        openPanel.gameObject.SetActive(false);
    }

    private IEnumerator ShowPanel(){
        Color panelColor = closePanel.color;
        float deltaAlpha = 0.01f;
        closePanel.gameObject.SetActive(true);        
        while (panelColor.a < 255)
        {
            panelColor.a += deltaAlpha;
            closePanel.color = panelColor;
            yield return new WaitForSecondsRealtime(0.0005f);
        }
    }
}

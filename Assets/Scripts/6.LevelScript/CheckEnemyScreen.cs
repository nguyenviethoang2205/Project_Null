using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckEnemyScreen : MonoBehaviour
{
    public Boards boards;
    public Text enemyName;
    public Text enemyDifficulty;
    public Text enemySurvive;
    public Text enemySkillDetail;
    public Image enemyImage;
    public bool isPause = false;

    public void Start(){
        enemyImage.sprite = boards.enemyCore.EnemyImage;
        enemyName.text = boards.enemyCore.EnemyName;
        enemyDifficulty.text = boards.enemyCore.EnemyDifficulty;
        enemySkillDetail.text = boards.enemyCore.EnemyDetail;
        enemySurvive.text = CheckSurviveStatus(boards.enemyCore.EnemyHealth);

        if (boards.enemyCore.EnemyDifficulty == "Easy"){
            TurnGreen(enemyDifficulty);
        } else if (boards.enemyCore.EnemyDifficulty == "Normal"){
            TurnYellow(enemyDifficulty);
        } else if (boards.enemyCore.EnemyDifficulty == "Hard"){
            TurnRed(enemyDifficulty);
        } else {
            TurnGray(enemyDifficulty);
        }

        if (enemySurvive.text == "Low"){
            TurnGreen(enemySurvive);
        } else if (enemySurvive.text == "Normal"){
            TurnYellow(enemySurvive);
        } else if (enemySurvive.text == "High"){
            TurnRed(enemySurvive);
        } else {
            TurnGray(enemySurvive);
        }
    }

    private string CheckSurviveStatus(int enemyHealth){
        if (enemyHealth < 150){
            return "Low";
        } else if (enemyHealth < 300){
            return "Normal";
        } else if (enemyHealth < 600){
            return "High";
        } else {
            return "THE WALL";
        }
    }

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

    
    private void TurnGreen(Text text){
        Color color = new Color(0f, 0.8f, 0.2196f);
        text.color = color;
    }

    private void TurnYellow(Text text){
        Color color = new Color(0.8f, 0.7529f, 0.1647f, 1f);
        text.color = color;
    }

    private void TurnRed(Text text){
        Color color = new Color(0.8f, 0.1137f, 0.1098f, 1f);
        text.color = color;
    }
    
    public void TurnBlue(Text text){
        Color color = new Color(0.15f, 0.76f, 0.8f);
        text.color = color;
    }
    
    public void TurnGray(Text text){
        Color color = new Color(0.4f, 0.4f, 0.4f);
        text.color = color;
    }
}

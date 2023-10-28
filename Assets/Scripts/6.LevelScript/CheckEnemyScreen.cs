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
        enemyDifficulty.text = "Difficulty: " + boards.enemyCore.EnemyDifficulty;
        enemySkillDetail.text = boards.enemyCore.EnemyDetail;
        enemySurvive.text = "Survive: " + CheckSurviveStatus(boards.enemyCore.EnemyHealth);
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
}

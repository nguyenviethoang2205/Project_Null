using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnimationUIManager : MonoBehaviour
{
    public EnemyCore enemyCore;
    public Boards boards;
    //-------------------------
    public Text enemyDisplayName;
    public Text textCombo;
    public Text textComboCount;
    public Text textTotalDamage;
    public Text textTotalDamageCount;
    // -------------------------
    public Text textDamage1;
    public Text textDamage2;
    public Text textDamage3;
    public Text textDamage4;
    private int choose = 0;

    private void Start(){
        ChangeName();
    }

    public void ShowDamageCombo(){

        if (boards.totalCombo < 2){
            StartCoroutine(CloseCombo());
        } else {
            ShowCombo();
        }
    }

    public void ChooseDamageToShow(){
        if (choose == 0){
            choose++;
            StartCoroutine(ShowDamage());
        } else if (choose == 1) {
            choose++;
            StartCoroutine(ShowDamage2());
        } else if (choose == 2) {
            choose++;
            StartCoroutine(ShowDamage3());
        } else {
            choose = 0;;
            StartCoroutine(ShowDamage4());
        }
    }

    private void ChangeName(){
        enemyDisplayName.text = enemyCore.EnemyName;
    }

    private void ShowCombo(){
        textCombo.gameObject.SetActive(true);
        textComboCount.gameObject.SetActive(true);
        textTotalDamage.gameObject.SetActive(true);
        textTotalDamageCount.gameObject.SetActive(true);
        
        textComboCount.text = boards.totalCombo.ToString();
        textTotalDamageCount.text = boards.totalDamageWithCombo.ToString();
        Color textComboColor = textCombo.color;
        Color textTotalDamageColor = textTotalDamage.color;

        textComboColor.a += 1f;
        textTotalDamageColor.a += 1f;
        textCombo.color = textComboColor;
        textComboCount.color = textComboColor;
        textTotalDamage.color = textTotalDamageColor;
        textTotalDamageCount.color = textTotalDamageColor;
    }

    IEnumerator CloseCombo()
    {
        Color textComboColor = textCombo.color;
        Color textTotalDamageColor = textTotalDamage.color;
        float deltaAlpha = 0.05f;
        while (textComboColor.a > 0f){
            textComboColor.a -= deltaAlpha;
            textTotalDamageColor.a -= deltaAlpha;
            textCombo.color = textComboColor;
            textComboCount.color = textComboColor;
            textTotalDamage.color = textTotalDamageColor;
            textTotalDamageCount.color = textTotalDamageColor;
            yield return new WaitForSecondsRealtime(0.000001f);
        }
        textCombo.gameObject.SetActive(false);
        textComboCount.gameObject.SetActive(false);
        textTotalDamage.gameObject.SetActive(false);
        textTotalDamageCount.gameObject.SetActive(false);
    }

    IEnumerator ShowDamage(){
        textDamage1.text = "-" + boards.damageLastTurn.ToString();
        Color textDamageColor = textDamage4.color;
        textDamageColor.a += 1f;
        textDamage1.color = textDamageColor;
        float deltaAlpha = 0.05f;
        float y = 3f;
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 startPosition = textDamage1.transform.position;
        while (textDamageColor.a > 0f){
            y += 0.02f;
            textDamage1.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            textDamageColor.a -= deltaAlpha;
            textDamage1.color = textDamageColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.001f);
        y = y - 0.4f;
        textDamage1.transform.position = new Vector3(startPosition.x, y, startPosition.z);
    }
    
    IEnumerator ShowDamage2(){
        textDamage2.text = "-" + boards.damageLastTurn.ToString();
        Color textDamageColor = textDamage2.color;
        textDamageColor.a += 1f;
        textDamage2.color = textDamageColor;
        float deltaAlpha = 0.05f;
        float y = 1f;
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 startPosition = textDamage2.transform.position;
        while (textDamageColor.a > 0f){
            y += 0.02f;
            textDamage2.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            textDamageColor.a -= deltaAlpha;
            textDamage2.color = textDamageColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.001f);
        y = y - 0.4f;
        textDamage2.transform.position = new Vector3(startPosition.x, y, startPosition.z);  yield return new WaitForSecondsRealtime(0.001f);
    }

    IEnumerator ShowDamage3(){
        textDamage3.text = "-" + boards.damageLastTurn.ToString();
        Color textDamageColor = textDamage3.color;
        textDamageColor.a += 1f;
        textDamage3.color = textDamageColor;
        float deltaAlpha = 0.05f;
        float y = -3f;
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 startPosition = textDamage3.transform.position;
        while (textDamageColor.a > 0f){
            y += 0.02f;
            textDamage3.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            textDamageColor.a -= deltaAlpha;
            textDamage3.color = textDamageColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.001f);
        y = y - 0.4f;
        textDamage3.transform.position = new Vector3(startPosition.x, y, startPosition.z);
    }

    IEnumerator ShowDamage4(){
        textDamage4.text = "-" + boards.damageLastTurn.ToString();
        Color textDamageColor = textDamage4.color;
        textDamageColor.a += 1f;
        textDamage4.color = textDamageColor;
        float deltaAlpha = 0.05f;
        float y = -1f;
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 startPosition = textDamage4.transform.position;
        while (textDamageColor.a > 0f){
            y += 0.02f;
            textDamage4.transform.position = new Vector3(startPosition.x, y, startPosition.z);
            textDamageColor.a -= deltaAlpha;
            textDamage4.color = textDamageColor;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        yield return new WaitForSecondsRealtime(0.001f);
        y = y - 0.4f;
        textDamage4.transform.position = new Vector3(startPosition.x, y, startPosition.z);
    }
}

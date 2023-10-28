using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAnimationUIManager : MonoBehaviour
{
    public EnemyCore enemyCore;
    public ComboBar comboBar;
    public Boards boards;
    public EnergyBar energyBar;
    public SpriteRenderer skillImage;
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
        skillImage.sprite = boards.character.skillImage;
        ChangeName();
    }

    public void EnergyNotFull(){
        energyBar.TurnWhite();
    }

    public void EnergyFull(){
        energyBar.TurnBlue();
    }

    public void SkillCanUse(){
        Color skillImageColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        skillImage.color = skillImageColor;
    }

    public void SkillCannotUse(){
        Color skillImageColor = new Color(1.0f, 1.0f, 1.0f, 0.6f);
        skillImage.color = skillImageColor;
    }

    public void SetEnergy(int energy){
        energyBar.SetEnergyValue(energy);
    }

    public void SetMaxEnergy(int energy){
        energyBar.SetMaxEnergyValue(energy);
    }

    public void ShowDamageCombo(){
        if (boards.totalCombo >= 2){
            ShowCombo();
        } else {
            comboBar.HideSlide();
            CloseCombo();
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

    public void UpdateMaxComboWait(int comboLost){
        comboBar.SetMaxComboValue(comboLost);
    }

    public void UpdateComboWait(int comboLost){
        comboBar.SetComboValue(comboLost);
    }

    public void ComboTurnWhite(){
        comboBar.TurnWhite();
        Color color = new Color(1f, 1f, 1f);
        textTotalDamage.color = color;
        textTotalDamageCount.color = color;
    }

    public void ComboTurnYellow(){
        comboBar.TurnYellow();
        Color color = new Color(1f, 0.9529f, 0.3647f);
        textTotalDamage.color = color;
        textTotalDamageCount.color = color;
    }

    public void ComboTurnOrange(){
        comboBar.TurnOrange();
        Color color = new Color(1f, 0.52f, 0.29f);
        textTotalDamage.color = color;
        textTotalDamageCount.color = color;
    }

    public void ComboTurnRed(){
        comboBar.TurnRed();
        Color color = new Color(1f, 0.3137f, 0.3098f);
        textTotalDamage.color = color;
        textTotalDamageCount.color = color;
    }

    private void ChangeName(){
        enemyDisplayName.text = enemyCore.EnemyName;
    }

    private void ShowCombo(){
        comboBar.ShowSlide();
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

    private void CloseCombo()
    {
        textCombo.gameObject.SetActive(false);
        textComboCount.gameObject.SetActive(false);
        textTotalDamage.gameObject.SetActive(false);
        textTotalDamageCount.gameObject.SetActive(false);
    }

    // IEnumerator CloseCombo()
    // {
    //     Color textComboColor = textCombo.color;
    //     Color textTotalDamageColor = textTotalDamage.color;
    //     float deltaAlpha = 0.1f;
    //     while (textComboColor.a > 0f){
    //         textComboColor.a -= deltaAlpha;
    //         textTotalDamageColor.a -= deltaAlpha;
    //         textCombo.color = textComboColor;
    //         textComboCount.color = textComboColor;
    //         textTotalDamage.color = textTotalDamageColor;
    //         textTotalDamageCount.color = textTotalDamageColor;
    //         yield return new WaitForSecondsRealtime(0.001f);
    //     }
    //     textCombo.gameObject.SetActive(false);
    //     textComboCount.gameObject.SetActive(false);
    //     textTotalDamage.gameObject.SetActive(false);
    //     textTotalDamageCount.gameObject.SetActive(false);
    // }

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

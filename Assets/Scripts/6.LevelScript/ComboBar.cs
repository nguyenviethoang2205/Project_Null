using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ComboBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public void SetMaxComboValue(int combo){
        slider.maxValue = combo;
        slider.value = combo;
    }
    
    public void SetComboValue(int combo){
        slider.value = combo;
    }

    public void TurnOrange(){
        Color color = new Color(1f, 0.52f, 0.29f);
        fill.color = color;
    }

    public void TurnYellow(){
        Color color = new Color(1f, 0.9529f, 0.3647f, 1f);
        fill.color = color;
    }

    public void TurnRed(){
        Color color = new Color(1f, 0.3137f, 0.3098f, 1f);
        fill.color = color;
    }

    
    public void TurnWhite(){
        Color color = new Color(1f, 1f, 1f);
        fill.color = color;
    }

    public void HideSlide(){
        Color fillColor = fill.color;
        fillColor.a -= 1f;
        fill.color = fillColor;
        fill.gameObject.SetActive(false);
    }
    

    public void ShowSlide(){
        fill.gameObject.SetActive(true);
        Color fillColor = fill.color;
        fillColor.a += 1f;
        fill.color = fillColor;
    }

}

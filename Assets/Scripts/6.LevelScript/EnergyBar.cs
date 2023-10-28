using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnergyBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public void SetMaxEnergyValue(int combo){
        slider.maxValue = combo;
        slider.value = combo;
    }
    
    public void SetEnergyValue(int combo){
        slider.value = combo;
    }

    public void TurnBlue(){
        Color color = new Color(0.35f, 0.96f, 1f);
        fill.color = color;
    }
    
    public void TurnWhite(){
        Color color = new Color(1f, 1f, 1f);
        fill.color = color;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public void SetMaxSkillValue(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetSkillValue(int health){
        slider.value = health;
    }

    public void TurnWhite(){
        Color color = new Color(1f, 1f, 1f);
        fill.color = color;
    }

    public void TurnRed(){
        Color color = new Color(1f, 0.3137f, 0.3098f, 1f);
        fill.color = color;
    }
}

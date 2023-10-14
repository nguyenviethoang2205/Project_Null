using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health){
        slider.value = health;
    }

    public void TurnGreen(){
        Color color = new Color(0f, 1f, 0.4196f);
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
}

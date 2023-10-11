using UnityEngine;

public class Slime : MonoBehaviour{
    private int enemyHealth;
    private string enemyName;

    public void Initialize()
    {
        this.enemyHealth = 150;
        this.enemyName = "Slime-D";
    }

    public string getName(){return enemyName;}
    public int getHealth(){return enemyHealth;}
}


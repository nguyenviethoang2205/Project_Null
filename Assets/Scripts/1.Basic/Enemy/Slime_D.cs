using UnityEngine;

public class Slime_D : EnemyCore{ 

    public override void Awake(){
        getName();
        getHealth();
    }

    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine(int totalLineClear) {}
    
    public override string getName()
    {
        SetEnemyName("Old Slime");
        return EnemyName;
    }   

    public override int getHealth()
    {
        SetEnemyHealth(100);
        return EnemyHealth;
    }
}    


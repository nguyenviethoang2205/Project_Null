using UnityEngine;

public class Slime_D : EnemyCore{ 

    public override void Awake(){
        maxSkillWait = 4;
        skillWait = 3;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        getName();
        getHealth();
    }
    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine(){}
    
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


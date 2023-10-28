using UnityEngine;

public class Slime_D : EnemyCore{ 

    public override void Awake(){
        maxSkillWait = 0;
        skillWait = 0;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
                EnemyImage = Resources.Load<Sprite>("Enemy/Slime");
        getName();
        getHealth();
        getDetail();
        getDifficulty();
    }
    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine() {}
    
    public override string getName()
    {
        SetEnemyName("Old Slime");
        return EnemyName;
    }   

    public override int getHealth()
    {
        SetEnemyHealth(149);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("A normal Slime, quite useless and doesn't attack you, but you need to defeat it to proceed");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("Easy");
        return EnemyDifficulty;
    }
}    


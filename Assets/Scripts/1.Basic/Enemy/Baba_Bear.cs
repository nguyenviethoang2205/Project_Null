using UnityEngine;

public class Baba_Bear : EnemyCore{ 

    public override void Awake(){
        getName();
        getHealth();
    }
    public override void EnemySkill(){
        if (boards.totalLinesClear == 1){
            boards.MakeAGrayLine();
            boards.DoEnemyAttack();
        } else if (boards.totalLinesClear == 3){
            int count = 0;
            while (count < 2){
                boards.MakeAGrayLine();
                count++;
            }
            boards.DoEnemyAttack();
        }
    }

    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine(){
        EnemySkill();
    }
    
    public override string getName()
    {
        SetEnemyName("Baba Bear");
        return EnemyName;
    }   

    public override int getHealth()
    {
        SetEnemyHealth(200);
        return EnemyHealth;
    }
}    


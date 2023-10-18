// using System;
using UnityEngine;

public class Baba_Bear : EnemyCore{
    private int countLineSkill = 0;
    public override void Awake(){
        getName();
        getHealth();
    }
    public void EnemySkill(int totalLineClear)
    {
        countLineSkill = countLineSkill + totalLineClear;
        while ( countLineSkill>= 4 ){
            boards.MakeAGrayLine();
            boards.DoEnemyAttack();
            countLineSkill = countLineSkill - 4;
        }
    }

    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine(int totalLineClear)
    {
        EnemySkill(totalLineClear);
    }
    
    public override string getName()
    {
        SetEnemyName("Baba Bear");
        return EnemyName;
    }   

    public override int getHealth()
    {
        SetEnemyHealth(300);
        return EnemyHealth;
    }
}    


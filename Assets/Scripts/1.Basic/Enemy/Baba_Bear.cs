﻿// using System;
using UnityEngine;

public class Baba_Bear : EnemyCore{ 

    public override void Awake(){
        getName();
        getHealth();
    }
    public void EnemySkill(){
        int lines = boards.countLines;
        while ( lines - 4 >= 0 ){
            boards.MakeAGrayLine();
            boards.DoEnemyAttack();
            lines = lines - 4;
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
        SetEnemyHealth(300);
        return EnemyHealth;
    }
}    


// using System;
using UnityEngine;


public class Baba_Bear : EnemyCore{ 
    public int lastTotalLine = 0;
// public class Baba_Bear : EnemyCore{
//     private int countLineSkill = 0;
    public override void Awake(){
        maxSkillWait = 5;
        skillWait = 3;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        getName();
        getHealth();
    }

    public void EnemySkill()
    {
        int lines = boards.totalLines - lastTotalLine;
        skillWait = skillWait + lines;
        while ( skillWait / 5 >= 1){
            boards.MakeAGrayLine();
            boards.DoEnemyAttack();
            skillWait = skillWait - 5;
//     public void EnemySkill(int totalLineClear)
//     {
//         countLineSkill = countLineSkill + totalLineClear;
//         while ( countLineSkill>= 4 ){
//             boards.MakeAGrayLine();
//             boards.DoEnemyAttack();
//             countLineSkill = countLineSkill - 4;
        }

        lastTotalLine = boards.totalLines;
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
    }

    
    public override void CheckSkillStart(){}

    public override void CheckSkillSpawn(){}

    public override void CheckSkillClearLine()
    {
        // EnemySkill(totalLineClear);
        EnemySkill();
    }
    
    public override string getName()
    {
        SetEnemyName("Baba Bear");
        return EnemyName;
    }   

    public override int getHealth()
    {
        SetEnemyHealth(400);
        return EnemyHealth;
    }
}    


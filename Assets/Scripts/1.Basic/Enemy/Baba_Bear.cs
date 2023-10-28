// using System;
using UnityEngine;


public class Baba_Bear : EnemyCore{ 
    public int lastTotalLine = 0;

    public override void Awake(){
        maxSkillWait = 4;
        skillWait = 2;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        EnemyImage = Resources.Load<Sprite>("Enemy/Baba_Bear");
        getName();
        getHealth();
        getDetail();
        getDifficulty();
    }

    public void EnemySkill()
    {
        int lines = boards.totalLines - lastTotalLine;
        skillWait = skillWait + lines;
        while ( skillWait / 4 >= 1){
            boards.MakeAGrayLine();
            boards.DoEnemyAttack();
            skillWait = skillWait - 4;
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
        SetEnemyHealth(450);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("Baba Bear is not lenient with those who provoke him. Whenever you clear 4 Lines, Baba Bear will attack and push up one line.");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("Hard");
        return EnemyDifficulty;
    }
}    


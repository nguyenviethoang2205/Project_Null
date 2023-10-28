using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : EnemyCore
{
    public int lastTotalLine = 0;
    public override void Awake()
    {
        maxSkillWait = 5;
        skillWait = 2;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        EnemyImage = Resources.Load<Sprite>("Enemy/Mecha");
        getName();
        getHealth();
        getDetail();
        getDifficulty();
    }


    bool Phase2 = false;
    public void EnemySkill1()
    {
        int lines = boards.totalLines - lastTotalLine;
        skillWait = skillWait + lines;
        if (activeSkill2 == true){
            while (skillWait - 5 >= 0){
                if (boards.currentHealth <= (EnemyHealth * 2 / 3) && Phase2 == true){
                    boards.MakeAGrayLine();
                    Phase2 = false;
                } else {
                    int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax);
                    boards.deleteCollum(deleteCol);
                    Phase2 = true;
                }
                boards.DoEnemyAttack();
                skillWait = skillWait - 5;
            }
        } else {
            while (skillWait - 4 >= 0){
                if (boards.currentHealth <= (EnemyHealth * 2 / 3) && Phase2 == true){
                    boards.MakeAGrayLine();
                    Phase2 = false;
                } else {
                    int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax);
                    boards.deleteCollum(deleteCol);
                    deleteCol++;
                    Phase2 = true;
                }
                boards.DoEnemyAttack();
                skillWait = skillWait - 4;
            }
//     private int countLineSkill1 = 0;
//     public void EnemySkill1(int totalLineClear)
//     {
//         countLineSkill1 = countLineSkill1 + totalLineClear;
//         while (countLineSkill1 - 4 >= 0)
//         {
//             int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax);
//             for(int i = 0; i<1; i++)
//             {
//                 boards.deleteCollum(deleteCol);
//                 deleteCol++;
//             }
//             boards.DoEnemyAttack();
//             countLineSkill1 = countLineSkill1 - 4;
        }

        lastTotalLine = boards.totalLines;
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
    }

    bool activeSkill2 = true;
    
    public void EnemySkill2()
    {
        if (boards.currentHealth * 3 <= EnemyHealth && activeSkill2==true)
        {
            for (int i = 0; i < 5; i++)
                boards.MakeAGrayLine();
            activeSkill2 = false;
            maxSkillWait = 4;
            skillWait = 3;
            skillBar.SetMaxSkillValue(maxSkillWait);
            skillBar.SetSkillValue(skillWait);
        }
    }

    public override void CheckSkillStart() { }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine()
    {
        EnemySkill1();
        EnemySkill2();
    }

    public override string getName()
    {
        SetEnemyName("Pink-Mecha-Nam");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(600);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("Being an invulnerable creature. No one knows how to withstand his attacks. However, we only know not to mess with him.");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("NIGHTMARE");
        return EnemyDifficulty;
    }
}

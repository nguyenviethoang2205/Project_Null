using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Werewolf : EnemyCore
{
    public override void Awake()
    {
        getName();
        getHealth();
    }

    private int countLineSkill1 = 0;
    public void EnemySkill1(int totalLineClear)
    {
        countLineSkill1 = countLineSkill1 + totalLineClear;
        while (countLineSkill1 - 4 >= 0)
        {
            int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax);
            for(int i = 0; i<1; i++)
            {
                boards.deleteCollum(deleteCol);
                deleteCol++;
            }
            boards.DoEnemyAttack();
            countLineSkill1 = countLineSkill1 - 4;
        }
    }

    bool activeSkill2 = true;
    public void EnemySkill2()
    {
        if (Boards.currentHealth * 3 <= EnemyHealth && activeSkill2==true)
        {
            for (int i = 0; i < 5; i++)
                boards.MakeAGrayLine();
            activeSkill2 = false;
        }
    }

    public override void CheckSkillStart() { }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine(int totalLineClear)
    {
        EnemySkill1(totalLineClear);
        EnemySkill2();
    }

    public override string getName()
    {
        SetEnemyName("Pink-Mecha-Nam");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(400);
        return EnemyHealth;
    }
}

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
    public void EnemySkill1()
    {
        int lines = boards.countLines;
        while (lines - 4 >= 0)
        {
            int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax);
            for(int i = 0; i<1; i++)
            {
                boards.deleteCollum(deleteCol);
                deleteCol++;
            }
            boards.DoEnemyAttack();
            lines = lines - 4;
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
        SetEnemyHealth(400);
        return EnemyHealth;
    }
}

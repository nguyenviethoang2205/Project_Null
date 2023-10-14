using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyCore
{
    public override void Awake()
    {
        getName();
        getHealth();
    }
    public void EnemySkill()
    {
        int lines = boards.countLines;
        while (lines - 3 >= 0)
        {
            int deleteCol = Random.Range(boards.Bounds.xMin, boards.Bounds.xMax - 2);
            for(int i = 0; i<3; i++)
            {
                boards.deleteCollum(deleteCol);
                deleteCol++;
            }
            boards.DoEnemyAttack();
            lines = lines - 3;
        }
    }


    public override void CheckSkillStart() { }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine()
    {
        EnemySkill();
    }

    public override string getName()
    {
        SetEnemyName("Boss");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(300);
        return EnemyHealth;
    }
}

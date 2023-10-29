using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Fusion: EnemyCore
{
    public override void Awake()
    {
        maxSkillWait = 5;
        skillWait = 3;
        skillBar.SetMaxSkillValue(maxSkillWait);
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
        EnemyImage = Resources.Load<Sprite>("Enemy/Slime_Fusion");
        getName();
        getHealth();
        getDetail();
        getDifficulty();
    }

    public void EnemySkill(){
        if (boards.activePiece.status == "dizzy"){
            maxSkillWait = 2;
            skillBar.SetMaxSkillValue(maxSkillWait);
            skillWait = 1;
            skillBar.SetSkillValue(skillWait);
            if (boards.totalLinesClear > 0){
                boards.HealInt(40);
            }
            skillWait--;
            if (skillWait <= 0){
                maxSkillWait = 5;
                skillWait = 0;
                skillBar.SetSkillValue(skillWait);
                skillBar.SetMaxSkillValue(maxSkillWait);
                boards.activePiece.getStatus("normal");
            }
        } else {
            if (skillWait == maxSkillWait){
                boards.DoEnemyAttack();
                maxSkillWait = 2;
                skillWait = 1;
                skillBar.SetSkillValue(skillWait);
                skillBar.SetMaxSkillValue(maxSkillWait);
                boards.activePiece.getStatus("dizzy");
            } else {
                skillWait++;
                if (boards.currentHealth > 1){
                    boards.ItemsDealDamage(1);
                }
            }
        }
        skillBar.SetSkillValue(skillWait);
        CheckStatus();
    }
    public override void CheckSkillStart() { }

    public override void CheckSkillSpawn() { }

    public override void CheckSkillClearLine()
    {
        EnemySkill();
    }

    public override string getName()
    {
        SetEnemyName("Alien Slime");
        return EnemyName;
    }

    public override int getHealth()
    {
        SetEnemyHealth(200);
        return EnemyHealth;
    }

    public override string getDetail()
    {
        SetEnemyDetail("Being a disciple of some Dryad, however, he always uses his magic incorrectly. He constantly takes 1 damage when blocks fall, until a certain point when he will make you 'dizzy' for a short time. When you're 'dizzy,' if you clear 1 line, he will restore a certain amount of health");
        return EnemyDetail;
    }

    public override string getDifficulty()
    {
        SetEnemyDifficulty("Normal");
        return EnemyDifficulty;
    }
}
